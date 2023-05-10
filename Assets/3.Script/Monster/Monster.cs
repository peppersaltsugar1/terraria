using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform playerTransform;  // 플레이어 Transform

    public float moveSpeed= 3;    // 몬스터 이동 속도
    public float maxSpeed = 5f;
    private bool monsterLive = true;
    public int monsterMaxHp;
    public int monsterCurrenHp;
    public int monsterAtk;
    public int monsterDef;
    public int monsterKnock;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D monRigid;
    [SerializeField] private GameObject copperPrefab;
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject monsterToolTip;


    [SerializeField] private AudioSource audioSource;
  



    private void Awake()
    {
        monsterCurrenHp = monsterMaxHp;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        int blockLayer = LayerMask.NameToLayer("Block");
        int monsterLayer = LayerMask.NameToLayer("Monster");
        int playerLayer = LayerMask.NameToLayer("Default");
        int deadLayer = LayerMask.NameToLayer("Corpse");
        Physics2D.IgnoreLayerCollision(monsterLayer, blockLayer);
        Physics2D.IgnoreLayerCollision(deadLayer, playerLayer);
        Physics2D.IgnoreLayerCollision(deadLayer, monsterLayer);
    }
    private void Update()
    {
        if (monsterLive)
        {
            Vector2 direction = playerTransform.position - transform.position;
            transform.right = direction;

            // 플레이어 방향으로 힘을 주기
            monRigid.AddForce(transform.right, ForceMode2D.Force);
            if (monRigid.velocity.magnitude > maxSpeed)
            {
                monRigid.velocity = monRigid.velocity.normalized * maxSpeed;
            }
        }

    }

    public void TakeDamage(int damage)
    {
        monsterCurrenHp -=(damage - monsterDef);
        if (monsterCurrenHp <= 0)
        {
            Die();
        }
        Debug.Log(monsterCurrenHp);
    }
    //public void AddKnock(int knock)
    //{
    //    Debug.Log("밀려남");
    //    monRigid.AddForce(new Vector2(knock, 0), ForceMode2D.Force);
    //}

    private IEnumerator Die_co()
    {

        animator.SetBool("isDie", true);
        transform.gameObject.layer = 11;
        monRigid.gravityScale = 1;
        DropItem(); // 아이템 드롭
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false); // 5초 후 몬스터 사라짐
    }

    private void Die()
    {
        // 사망 처리
        monsterLive = false;
        StartCoroutine(Die_co());
    }
    public void DropItem()
    {
        int drop = Random.Range(2, 3);
        switch (drop)
        {
            case 1: Instantiate(copperPrefab, transform.position, Quaternion.identity); break;
            case 2: Instantiate(copperPrefab, transform.position, Quaternion.identity);Instantiate(core, transform.position, Quaternion.identity); break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(monsterAtk);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(monsterAtk);
        }
    }
}
