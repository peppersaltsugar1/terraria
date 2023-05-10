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
        Debug.Log("��ư����");
        if (eventData.button == PointerEventData.InputButton.Left&&useUpgrade==false)
        {
            Debug.Log("���������");
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
                if(barNum == 1) //�ӽ÷� 1�θ��� 20�����ؾ���
                {
                    break;
                }
               
            }
            Debug.Log("�κ�üũ�µ�");
            if (barNum >= 1)//�ӽ÷� 1�θ��� 20�����ؾ���
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
                    Debug.Log("�ٲٴ½ĵ� ��������");
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
                        case "CopperBar": GameManager.Instance.playerControl.weaponLevel = 2; Debug.Log("��������"); break;
                        case "IronBar": GameManager.Instance.playerControl.weaponLevel = 3; break;
                        case "TungstenBar": GameManager.Instance.playerControl.weaponLevel = 4; break;
                        case "GoldBar": GameManager.Instance.playerControl.weaponLevel = 5; break;
                        case "OrichalcumBar": GameManager.Instance.playerControl.weaponLevel = 6; break;
                    }
                    GameManager.Instance.playerControl.UpgradeLevel();
                    useUpgrade = true;
                    Debug.Log("���̰Թ����ļ���");
                    buttonText = transform.Find("Text").GetComponent<Text>();
                    Debug.Log("����");
                    GameManager.Instance.playerLevel.PlayerLevelView();
                    buttonText.text = "Complete";
                    return;
                }
                
            }
        }
    }
}
