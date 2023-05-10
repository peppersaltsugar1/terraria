using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotItemName : MonoBehaviour
{
    
    private Text quickSlotItemName;

    private void Awake()
    {
        quickSlotItemName = transform.gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        if(Inven.Instance.useQuickSlotIndex!=-1)
        {
            if(Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] != null)
            {
                quickSlotItemName.text = Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex].name.ToString();
            }
            else if(Inven.Instance.quickSlot[Inven.Instance.useQuickSlotIndex] == null)
            {
                quickSlotItemName.text = "";
            }
        }

    }
}
