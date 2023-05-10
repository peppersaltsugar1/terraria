using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private bool moveCheck;
    private int moveIndex;
    private string moveName;
    private bool tempCheck;
    private int tempIndex;
    private string tempName;
    private ItemSlot itemslot;
    private void Start()
    {
        itemslot = GetComponent<ItemSlot>();
        if(Inven.Instance.inventory[itemslot.invenIndex] != null&& moveCheck == false)
        {
            moveIndex = itemslot.invenIndex;
            moveName = Inven.Instance.inventory[itemslot.invenIndex].name;
            Inven.Instance.inventory[itemslot.invenIndex] = null;
            moveCheck = true;

        }
        if(Inven.Instance.inventory[itemslot.invenIndex] == null&& moveCheck == true)
        {
            Inven.Instance.AddInven(moveName);
            moveCheck = true;

        }

    }
   
}
