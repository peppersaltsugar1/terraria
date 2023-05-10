using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject useBar;
    private bool useUpgrade;
    private int barNum;
    private Text buttonText;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("버튼눌림");
        if (eventData.button == PointerEventData.InputButton.Left&&useUpgrade==false)
        {
            Debug.Log("조건통과됨");
            barNum = 0;
            Debug.Log(useBar.name);
            for(int i = 0; i < Inven.Instance.inventory.Count; i++)
            {
                if (Inven.Instance.inventory[i] == null)
                {
                    continue;
                }
                if (Inven.Instance.inventory[i].name == useBar.name)
                {
                    barNum++;
                }
                if(barNum == 1) //임시로 1로만듬 20으로해야함
                {
                    break;
                }
               
            }
            Debug.Log("인벤체크는됨");
            if (barNum >= 1)//임시로 1로만듬 20으로해야함
            {
                for (int i = 0; i < Inven.Instance.inventory.Count; i++)
                {
                    if(barNum <= 0)
                    {
                        break;
                    }
                    if (Inven.Instance.inventory[i] == null)
                    {
                        continue;
                    }
                    Debug.Log("바꾸는식도 시작은됨");
                    Debug.Log(useBar.name);
                    if (Inven.Instance.inventory[i].name == useBar.name)
                    {
                        Inven.Instance.inventory[i] = null;
                        barNum--;
                    }
                }
                Debug.Log(useBar.name);
                if (barNum <= 0)
                {
                    Debug.Log(useBar.name);
                    switch (useBar.name)
                    {
                        case "CopperBar": GameManager.Instance.playerControl.weaponLevel = 2; Debug.Log("레벨업됨"); break;
                        case "IronBar": GameManager.Instance.playerControl.weaponLevel = 3; break;
                        case "TungstenBar": GameManager.Instance.playerControl.weaponLevel = 4; break;
                        case "GoldBar": GameManager.Instance.playerControl.weaponLevel = 5; break;
                        case "OrichalcumBar": GameManager.Instance.playerControl.weaponLevel = 6; break;
                    }
                    GameManager.Instance.playerControl.UpgradeLevel();
                    useUpgrade = true;
                    Debug.Log("또이게문제냐설마");
                    buttonText = transform.Find("Text").GetComponent<Text>();
                    Debug.Log("설마");
                    GameManager.Instance.playerLevel.PlayerLevelView();
                    buttonText.text = "Complete";
                    return;
                }
                
            }
        }
    }
}
