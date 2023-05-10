using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f; //�÷��̾ ����� �÷��̾�� ������� �ӵ�
    [SerializeField] float pickUpDistance = 2f; //�����۽������
    [SerializeField] GameObject item;
    private string itemName;
    [TextArea]
    public string itemDesc;
    

    private void Awake()
    {
        player = GameManager.Instance.playerControl.transform;
        itemName = item.name.Replace("(Clone)","");
    }
    void Start()
    {
        // ���̾� ����ũ ����
        int playerLayer = LayerMask.NameToLayer("Default");
        int itemLayer = LayerMask.NameToLayer("Item");
        Physics2D.IgnoreLayerCollision(itemLayer, itemLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, itemLayer);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        //transform.position �� �������� ��ġ, player.position�� �÷��̾� ��ġ
        //Vector3.Distance(a,b) => a�� b ������ �Ÿ� ��ȯ
        if (Inven.Instance.invenUse==true)
        {
            if (distance > pickUpDistance)
            {//������ ���� �������� �ָ� ĳ���Ͱ� �ִ°�� 
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //������ ��ġ�� �÷��̾�� �̵� , �ӷ�(3��° �Ű�����)
            //time.deltaTime => ��ǻ�� ������ ������ �ٸ��Ƿ� (1�ʿ� � ���� 20������ ����� 10������) �̸� �����ϰ� �ϴ� �Լ�

            if (distance < 1f)
            { //��ü�� ĳ���ͷ� �̵��ϸ鼭 �Ÿ��� ���� �̸��Ǹ� �������� �����ϰ� �����ۻ���
                Destroy(gameObject);
                Inven.Instance.AddInven(itemName);
            }
        }

    }
    


        

}

