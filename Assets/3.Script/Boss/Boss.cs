using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Transform playerTransform;  // �÷��̾� Transform

    public float moveSpeed = 10;    // ���� �̵� �ӵ�
    public float maxSpeed = 15f;
    private bool monsterLive = true;
    public int monsterMaxHp;
    public int monsterCurrenHp;
    public int monsterAtk;
    public int monsterDef;
    public int monsterKnock;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D monRigid;
    [SerializeField] private GameObject bossCore;
    [SerializeField] private GameObject bossHp;


    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip audioSource;
    //[SerializeField] private AudioClip audioSource;
    //[SerializeField] private AudioClip audioSource;



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

            // �÷��̾� �������� ���� �ֱ�
            monRigid.AddForce(transform.right*10, ForceMode2D.Force);
            if (monRigid.velocity.magnitude > maxSpeed)
            {
                monRigid.velocity = monRigid.velocity.normalized * maxSpeed;
            }
        }

    }

    public void TakeDamage(int damage)
    {
        monsterCurrenHp -= (damage - monsterDef);

        if((float)monsterCurrenHp/monsterMaxHp <= 0.5)
        {
            animator.SetBool("Phase2", true);
        }
        if (monsterCurrenHp <= 0)
        {
            Die();
        }
    }
    //public void AddKnock(int knock)
    //{
    //    Debug.Log("�з���");
    //    monRigid.AddForce(new Vector2(knock, 0), ForceMode2D.Force);
    //}

    private IEnumerator Die_co()
    {

        animator.SetBool("isDie", true);
        transform.gameObject.layer = 11;
        monRigid.gravityScale = 1;
        DropItem(); // ������ ���
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        bossHp.SetActive(false);// 5�� �� ���� �����
    }

    private void Die()
    {
        // ��� ó��
        monsterLive = false;
        StartCoroutine(Die_co());
    }
    public void DropItem()
    {
       Instantiate(bossCore, transform.position, Quaternion.identity);
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
