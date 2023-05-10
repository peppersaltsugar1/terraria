using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelting : MonoBehaviour
{
    [SerializeField] private GameObject SmeltingMineral;
    public int mineralNum;
    public int useMineralCount;

    




    public void SmeltingBar()
    {
        Debug.Log("실행됨");
        Debug.Log(SmeltingMineral.name);
        mineralNum = 0;
        useMineralCount = 0;
        Debug.Log(Inven.Instance.inventory.Count);
        for (int i = 0; i < Inven.Instance.inventory.Count; i++)
        {
            
            Debug.Log("이거실행되나");
            if (Inven.Instance.inventory[i] == null)
            {
                continue;
            }
            if(Inven.Instance.inventory[i].name == SmeltingMineral.name)
            {
                mineralNum++;
                Debug.Log(mineralNum);
            }
            if (mineralNum >= 3)
            {
                Debug.Log("3개모임");
                break;
            }
        }
        if (mineralNum < 3)
        {
            return;
        }
        for(int i = 0; i < Inven.Instance.inventory.Count; i++)
        {
            if (Inven.Instance.inventory[i] == null)
            {
                continue;
            }
            if (Inven.Instance.inventory[i].name == SmeltingMineral.name)
            {
                Inven.Instance.inventory[i] = null;
                useMineralCount++;
            }
            if(useMineralCount == 3)
            {
                Inven.Instance.InvenCheck();
                Inven.Instance.AddInven(SmeltingMineral.name + "Bar");
                return;
            }

        }

    }
}
