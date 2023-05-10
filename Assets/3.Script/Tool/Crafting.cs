using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private GameObject useCore;
    [SerializeField] private GameObject useBar;
    [SerializeField] private GameObject BossSpawner;
    private bool isUseCore;
    private bool isUseBar;
    private int useCoreNum;
    private int useBarNum;

    public void CraftBossSpawner()
    {
        useCoreNum = 0;
        useBarNum = 0;
        for (int i = 0; i < Inven.Instance.inventory.Count; i++)
        {
            if (Inven.Instance.inventory[i] == null)
            {
                continue;
            }
            if (Inven.Instance.inventory[i].name == useBar.name)
            {
                useBarNum++;
            }
            if (useBarNum == 1) //임시로 1로만듬 10으로해야함
            {
                break;
            }
          
        }
        for(int i = 0; i < Inven.Instance.inventory.Count; i++)
        {
            if (Inven.Instance.inventory[i] == null)
            {
                continue;
            }
            if (Inven.Instance.inventory[i].name == useCore.name)
            {
                useCoreNum++;
            }
            if (useCoreNum == 1) //임시로 1로만듬 10으로해야함
            {
                break;
            }
        }

        if (useBarNum >= 1)//임시로 1로만듬 10으로해야함
        {
            for (int i = 0; i < Inven.Instance.inventory.Count; i++)
            {
                if (useBarNum <= 0)
                {
                    isUseBar = true;
                    break;
                }
                if (Inven.Instance.inventory[i] == null)
                {
                    continue;
                }
                if (Inven.Instance.inventory[i].name == useBar.name)
                {
                    Inven.Instance.inventory[i] = null;
                    useBarNum--;
                }
            }
        }
        if (useCoreNum >= 1)//임시로 1로만듬 10으로해야함
        {
            for (int i = 0; i < Inven.Instance.inventory.Count; i++)
            {
                if (useCoreNum <= 0)
                {
                    isUseCore = true;
                    break;
                }
                if (Inven.Instance.inventory[i] == null)
                {
                    continue;
                }
                if (Inven.Instance.inventory[i].name == useCore.name)
                {
                    Inven.Instance.inventory[i] = null;
                    useCoreNum--;
                }
            }
        }
        if (isUseBar && isUseCore)
        {
            Inven.Instance.InvenCheck();
            Inven.Instance.AddInven(BossSpawner.name);
            isUseBar = false;
            isUseCore = false;
        }

    }   
}
