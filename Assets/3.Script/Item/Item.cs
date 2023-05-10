using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f; //플레이어가 습득시 플레이어로 날라오는 속도
    [SerializeField] float pickUpDistance = 2f; //아이템습득범위
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
        // 레이어 마스크 설정
        int playerLayer = LayerMask.NameToLayer("Default");
        int itemLayer = LayerMask.NameToLayer("Item");
        Physics2D.IgnoreLayerCollision(itemLayer, itemLayer);
        Physics2D.IgnoreLayerCollision(playerLayer, itemLayer);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        //transform.position 은 아이템의 위치, player.position은 플레이어 위치
        //Vector3.Distance(a,b) => a와 b 사이의 거리 반환
        if (Inven.Instance.invenUse==true)
        {
            if (distance > pickUpDistance)
            {//아이템 습득 범위보다 멀리 캐릭터가 있는경우 
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //아이템 위치를 플레이어로 이동 , 속력(3번째 매개변수)
            //time.deltaTime => 컴퓨터 성능이 제각기 다르므로 (1초에 어떤 컴은 20프레임 어떤컴은 10프레임) 이를 동일하게 하는 함수

            if (distance < 1f)
            { //물체가 캐릭터로 이동하면서 거리가 일정 미만되면 습득으로 간주하고 아이템삭제
                Destroy(gameObject);
                Inven.Instance.AddInven(itemName);
            }
        }

    }
    


        

}

