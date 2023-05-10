using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveSlot : MonoBehaviour
{
    public bool useMoveSlot;
    [SerializeField]private Image itemImage;
    public GameObject moveItem;
    public string moveItemName;
    public string tempName;
    private Rigidbody2D rigid;
    private GameObject dropItem;
    
    
    private void Update()
    {
        if (useMoveSlot)
        {
            rigid = moveItem.transform.GetComponent<Rigidbody2D>();
        } 
        if (useMoveSlot)
        {
            itemImage.color = new Color(1f, 1f, 1f, 1f); // 완전 불투명
        }
        else
        {
            itemImage.color = new Color(1f, 1f, 1f, 0f); // 완전 투명
        }

        // 이동 가능한 슬롯 위치 변경

        transform.position = Input.mousePosition;
       

    }

    public void moveSlotImageChange()
    {
        useMoveSlot = true;
        GameManager.Instance.item.TryGetValue(moveItemName, out moveItem);
        itemImage.sprite = moveItem.GetComponent<SpriteRenderer>().sprite;
    }

    public void DropItem()
    {
        dropItem = Instantiate(moveItem, GameManager.Instance.playerControl.transform.position, Quaternion.identity);
        rigid = dropItem.transform.GetComponent<Rigidbody2D>();
        Vector2 throwDirection = GameManager.Instance.playerControl.transform.right; // 플레이어가 바라보는 방향으로 던짐
        rigid.AddForce(throwDirection * 10, ForceMode2D.Impulse);
    }

    
    
}
