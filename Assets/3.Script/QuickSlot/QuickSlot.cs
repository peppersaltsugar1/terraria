using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int QuickIndex;
    [SerializeField] private Sprite standard;
    [SerializeField] private Sprite useQuickImage;
    public Image image;
    private GameObject inputItem;
    private Rigidbody2D rigid;
    private GameObject dropItem;
    public int itemCount = 0;
    public int quicSlotNum;
    public Text quickSlotNumText;
    public Image useQuickSlotImage;

    private void Start()
    {
        Debug.Log(QuickIndex);
        image = transform.Find("QuickSlotItem").GetComponent<Image>();
        useQuickSlotImage = gameObject.transform.GetComponent<Image>();
    }

    private void Update()
    {   
        if (Input.GetKey(KeyCode.Alpha1))
        {
            for(int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[0] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[1] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[2] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[3] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[4] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[5] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[6] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[7] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[8] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }
        else if (Input.GetKey(KeyCode.Alpha0))
        {
            for (int i = 0; i < 10; i++)
            {
                Inven.Instance.useQuickSlot[i] = false;
            }
            Inven.Instance.useQuickSlot[9] = true;
            Inven.Instance.UseQuickSlotIndexCheck();
        }

        if (Inven.Instance.useQuickSlot[QuickIndex])
        {
            useQuickSlotImage.sprite = useQuickImage;
        }
        else if (!Inven.Instance.useQuickSlot[QuickIndex])
        {
            useQuickSlotImage.sprite = standard;
        }

        if (Inven.Instance.quickSlot[QuickIndex] != null)
        {
            GameManager.Instance.item.TryGetValue(Inven.Instance.quickSlot[QuickIndex].name, out inputItem);
            image.sprite = inputItem.GetComponent<SpriteRenderer>().sprite;



        }
        if (Inven.Instance.quickSlot[QuickIndex] == null)
        {
            GameManager.Instance.item.TryGetValue("itemNull", out inputItem);
            image.sprite = inputItem.GetComponent<SpriteRenderer>().sprite;
        }

    }
    public void InputIndex(int i)
    {
        QuickIndex = i;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inven.Instance.quickSlot[QuickIndex] != null && GameManager.Instance.moveSlot.useMoveSlot == false)
            {
                GameManager.Instance.moveSlot.moveItemName = Inven.Instance.quickSlot[QuickIndex].name;
                GameManager.Instance.moveSlot.moveSlotImageChange();
                Inven.Instance.quickSlot[QuickIndex] = null;
            }
            else if (Inven.Instance.quickSlot[QuickIndex] != null && GameManager.Instance.moveSlot.useMoveSlot == true)
            {
                GameManager.Instance.moveSlot.tempName = Inven.Instance.quickSlot[QuickIndex].name;
                Inven.Instance.ChangeQuickSlot(QuickIndex, GameManager.Instance.moveSlot.moveItemName);
                GameManager.Instance.moveSlot.moveItemName = GameManager.Instance.moveSlot.tempName;
                GameManager.Instance.moveSlot.moveSlotImageChange();

            }
            else if (Inven.Instance.quickSlot[QuickIndex] == null && GameManager.Instance.moveSlot.useMoveSlot == true)
            {
                Inven.Instance.ChangeQuickSlot(QuickIndex, GameManager.Instance.moveSlot.moveItemName);
                GameManager.Instance.moveSlot.useMoveSlot = false;
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Inven.Instance.quickSlot[QuickIndex] != null)
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
                    Inven.Instance.quickSlot[QuickIndex] = null;
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
                    Inven.Instance.quickSlot[QuickIndex] = null;
                }
            }

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Inven.Instance.quickSlot[QuickIndex] != null)
        {
            GameManager.Instance.slotToolTip.ShowToolTip(Inven.Instance.quickSlot[QuickIndex]);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.slotToolTip.HideToolTip();
    }
}
