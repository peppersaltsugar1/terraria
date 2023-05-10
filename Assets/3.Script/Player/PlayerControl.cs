using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed = 10;
    private float jumpForce = 25;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] public SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    public int maxJumpEnergy = 1;
    public int jumCount = 0;
    public bool isAttack;
    public float attackCool = 0.5f;
    private float curTime;
    public Transform pos;
    public Vector2 boxSize;
    [SerializeField] private GameObject melee;
    public int weaponLevel = 1;
    public int playerAtk = 15;
    public int playerDef = 5;
    public int playerMaxHp = 100; 
    public int playerCurrenHp;
    public int playerKnock = 1000;
    private bool isInvincible;
    private float invincibleTime = 1.0f;
    private bool isDead;


   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip attackSound;
   [SerializeField] private AudioClip playerDead;
   [SerializeField] private AudioClip playerHit;


    private void Awake()
    {
        playerCurrenHp = playerMaxHp;
    }

    private void Update()
    {
        //플레이어가 사망상태가 아닐시 점프와 공격, 퀵슬롯에서 회복아이템을 사용하는 기능
        if (!isDead)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (jumCount < maxJumpEnergy)
                {
                    rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    jumCount++;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isAttack = !isAttack;
            }
            if (isAttack == true)
            {
                if (curTime <= 0)
                {
                    if (Input.GetMouseButton(0))
                    {
                        //마우스 버튼을눌렀을시 플레이어 앞에 지정한 히트박스 만큼 영역의 충돌을 감지함 그리고 해당 충돌객체가 몬스터이거나 보스이면 데미지를 가함
                        audioSource.PlayOneShot(attackSound);
                        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize,0);
                        foreach(Collider2D collider in collider2Ds)
                        {
                            if (collider.tag == "Monster" && collider.gameObject.layer != 11)
                            {
                                collider.GetComponent<Monster>().TakeDamage(playerAtk);
                            }
                            if (collider.tag == "Boss" && collider.gameObject.layer != 11)
                            {
                                collider.GetComponent<Boss>().TakeDamage(playerAtk);
                            }
                        }

                        animator.SetBool("isAttack", true);
                        curTime = attackCool;
                    }
                    if (!Input.GetMouseButton(0))
                    {
                        animator.SetBool("isAttack", false);
                    }
                }
                else
                {
                    curTime -= Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    animator.SetBool("isMin", true);
                }
                if (!Input.GetMouseButton(0))
                {
                    animator.SetBool("isMin", false);
                }
            }
            //퀵슬롯이 비어있지 않고 아이템바일경우 사용해서 체력을회복시킴
            if (Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] != null)
            {
                if (Input.GetMouseButton(0))
                {
                Heal(Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex].name);
                }
            }
        }
    }

    //플레이어 이동관리
    void FixedUpdate()
    {
        if (!isDead)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0)
            {
                animator.SetBool("isWalk", true);
                if (horizontalInput < 0)
                {
                    melee.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, 0);
                    sprite.flipX = false;
                }
                else if (horizontalInput > 0)
                {
                    melee.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, 0);
                    sprite.flipX = true;
                }

                rigid.velocity = new Vector2(horizontalInput * maxSpeed, rigid.velocity.y);
            }
            else
            {
                animator.SetBool("isWalk", false);
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }
        }
    }

    //플레이어의 점프카운트를 관리해서 연속점프가 안되게하고 땅을 밟앗을때 점프하게함
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigid.velocity.y == 0)
        {
            jumCount = 0;
        }
    }

    //히트박스를 눈으로 확인하기위해 기즈모를 사용함 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(pos.position, boxSize); 
    }

    //업그레이드기능
    public void UpgradeLevel()
    {
        switch (weaponLevel)
        {
            case 2 : playerAtk = 30; playerDef = 10; playerMaxHp = 120; playerCurrenHp = playerMaxHp; break;
            case 3 : playerAtk = 45; playerDef = 15; playerMaxHp = 140; playerCurrenHp = playerMaxHp; break;
            case 4 : playerAtk = 60; playerDef = 20; playerMaxHp = 160; playerCurrenHp = playerMaxHp; break;
            case 5 : playerAtk = 75; playerDef = 25; playerMaxHp = 180; playerCurrenHp = playerMaxHp; break;
            case 6 : playerAtk = 90; playerDef = 30; playerMaxHp = 200; playerCurrenHp = playerMaxHp; break;

        }

    }
    //플레이어가 몬스터와 접촉햇을시 피격판정이 일어나는부분
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            audioSource.PlayOneShot(playerHit);
            playerCurrenHp -= (damage - playerDef);
            Debug.Log(playerCurrenHp);
            if (playerCurrenHp <= 0)
            {
                Die();
                return;
            }
            StartCoroutine("HitColorAnimation");
            StartCoroutine("SetInvincibleState");
        }
    }

    //죽었을시 실행시킬 메소드
    private void Die()
    {
        audioSource.PlayOneShot(playerHit);
        StartCoroutine("PlayerDie_co");
    }

    //죽자마자 화면이전환되는것을 막기위해 코루틴을사용하여 딜레이를줌
    private IEnumerator PlayerDie_co()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        transform.gameObject.layer = 11;
        maxSpeed = 0;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Die");

    }
    //피격시 무적판정을 주기위해사용한 코루틴
    private IEnumerator SetInvincibleState()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
    //피격시 플레이어가 깜빡거리는것을 구현한 코루틴
    private IEnumerator HitColorAnimation()
    {
        for (int i = 0; i < 5; i++)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }

    //퀵슬롯에서 들고있던 주괴에따라서 체력을 회복시키는 메소드
    private void Heal(string BarName)
    {
        switch (BarName)
        {
            case "CopperBar":
                if (playerCurrenHp + 20 <= playerMaxHp)
                {
                    playerCurrenHp += 20;
                }
                else
                {
                    playerCurrenHp = playerMaxHp;
                }
                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                break;
            case "IronBar":
                if (playerCurrenHp + 25 <= playerMaxHp)
                {
                    playerCurrenHp += 25;
                }
                else
                {
                    playerCurrenHp = playerMaxHp;
                }
                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                break;
            case "TungstenBar":
                if (playerCurrenHp + 30 <= playerMaxHp)
                {
                    playerCurrenHp += 30;
                }
                else
                {
                    playerCurrenHp = playerMaxHp;
                }
                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                break;
            case "GoldBar":
                if (playerCurrenHp + 35 <= playerMaxHp)
                {
                    playerCurrenHp += 35;
                }
                else
                {
                    playerCurrenHp = playerMaxHp;
                }
                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                break;
            case "OrichalcumBar":

                if (playerCurrenHp + 40 <= playerMaxHp)
                {
                    playerCurrenHp += 40;
                }
                else
                {
                    playerCurrenHp = playerMaxHp;
                }
                Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] = null;
                break;

        }
    }

}
