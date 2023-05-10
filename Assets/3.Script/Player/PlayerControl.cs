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
        //�÷��̾ ������°� �ƴҽ� ������ ����, �����Կ��� ȸ���������� ����ϴ� ���
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
                        //���콺 ��ư���������� �÷��̾� �տ� ������ ��Ʈ�ڽ� ��ŭ ������ �浹�� ������ �׸��� �ش� �浹��ü�� �����̰ų� �����̸� �������� ����
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
            //�������� ������� �ʰ� �����۹��ϰ�� ����ؼ� ü����ȸ����Ŵ
            if (Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] != null)
            {
                if (Input.GetMouseButton(0))
                {
                Heal(Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex].name);
                }
            }
        }
    }

    //�÷��̾� �̵�����
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

    //�÷��̾��� ����ī��Ʈ�� �����ؼ� ���������� �ȵǰ��ϰ� ���� ������� �����ϰ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigid.velocity.y == 0)
        {
            jumCount = 0;
        }
    }

    //��Ʈ�ڽ��� ������ Ȯ���ϱ����� ����� ����� 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(pos.position, boxSize); 
    }

    //���׷��̵���
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
    //�÷��̾ ���Ϳ� ���������� �ǰ������� �Ͼ�ºκ�
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

    //�׾����� �����ų �޼ҵ�
    private void Die()
    {
        audioSource.PlayOneShot(playerHit);
        StartCoroutine("PlayerDie_co");
    }

    //���ڸ��� ȭ������ȯ�Ǵ°��� �������� �ڷ�ƾ������Ͽ� �����̸���
    private IEnumerator PlayerDie_co()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        transform.gameObject.layer = 11;
        maxSpeed = 0;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Die");

    }
    //�ǰݽ� ���������� �ֱ����ػ���� �ڷ�ƾ
    private IEnumerator SetInvincibleState()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
    //�ǰݽ� �÷��̾ �����Ÿ��°��� ������ �ڷ�ƾ
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

    //�����Կ��� ����ִ� �ֱ������� ü���� ȸ����Ű�� �޼ҵ�
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
