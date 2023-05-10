using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private GameObject upgradePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            upgradePanel.SetActive(true);
        }

    }
}
