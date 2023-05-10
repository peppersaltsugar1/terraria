using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{ 
    public int invenIndex;
    [SerializeField] private Sprite standard;
    public Image image;
    private GameObject inputItem;
    private Rigidbody2D rigid;
    private GameObject dropItem;
    public int itemCount = 0;

    private void Start()
    {
        image = transform.Find("Item").GetComponent<Image>();
    }

    private void Update()
    {
        if (Inven.Instance.inventory[invenIndex] != null)
        {
            GameManager.Instance.item.TryGetValue(Inven.Instance.inventory[invenIndex].name, out inputItem);
            image.sprite = inputItem.GetComponent<SpriteRenderer>().sprite;
        }
        if (Inven.Instance.inventory[invenIndex] == null)
        {
            GameManager.Instance.item.TryGetValue("itemNull", out inputItem);
            image.sprite = inputItem.GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void InputIndex(int i)
    {
        invenIndex = i;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inven.Instance.inventory[invenIndex] != null && GameManager.Instance.moveSlot.useMoveSlot == false)
            {
                GameManager.Instance.moveSlot.moveItemName = Inven.Instance.inventory[invenIndex].name;
                GameManager.Instance.moveSlot.moveSlotImageChange();
                Inven.Instance.inventory[invenIndex] = null;
            }
            else if (Inven.Instance.inventory[invenIndex] != null && GameManager.Instance.moveSlot.useMoveSlot == true)
            {
                GameManager.Instance.moveSlot.tempName = Inven.Instance.inventory[invenIndex].name;
                Inven.Instance.ChangeInven(invenIndex, GameManager.Instance.moveSlot.moveItemName);
                GameManager.Instance.moveSlot.moveItemName = GameManager.Instance.moveSlot.tempName;
                GameManager.Instance.moveSlot.moveSlotImageChange();

            }
            else if (Inven.Instance.inventory[invenIndex] == null && GameManager.Instance.moveSlot.useMoveSlot == true)
            {
                Inven.Instance.ChangeInven(invenIndex, GameManager.Instance.moveSlot.moveItemName);
                GameManager.Instance.moveSlot.useMoveSlot = false;
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Inven.Instance.inventory[invenIndex] != null)
            {
                if (GameManager.Instance.playerControl.sprite.flipX)
                {
                    float throwDistance = 2f; // 던지는 거리
                    Vector2 throwPosition = GameManager.Instance.playerControl.transform.position +
                                            GameManager.Instance.playerControl.transform.right * throwDistance;
                    dropItem = Instantiate(inputItem, throwPosition, Quaternion.identity);
                    rigid = dropItem.transform.GetComponent<Rigidbody2D>();
                    Vector2 throwDirection = GameManager.Instance.playerControl.transform.right;
                    rigid.AddForce(throwDirection * 2, ForceMode2D.Impulse);
                    Inven.Instance.inventory[invenIndex] = null;
                }
                else if (!GameManager.Instance.playerControl.sprite.flipX)
                {
                    float throwDistance = 2f; // 던지는 거리
                    Vector2 throwPosition = GameManager.Instance.playerControl.transform.position -
                                            GameManager.Instance.playerControl.transform.right * throwDistance; // 위치 계산 수정
                    dropItem = Instantiate(inputItem, throwPosition, Quaternion.identity);
                    rigid = dropItem.transform.GetComponent<Rigidbody2D>();
                    Vector2 throwDirection = -GameManager.Instance.playerControl.transform.right; // 힘의 방향도 반대로 수정
                    rigid.AddForce(throwDirection * 2, ForceMode2D.Impulse);
                    Inven.Instance.inventory[invenIndex] = null;
                }
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Inven.Instance.inventory[invenIndex] != null)
        {
            GameManager.Instance.slotToolTip.ShowToolTip(Inven.Instance.inventory[invenIndex]);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.slotToolTip.HideToolTip();
    }
}
