using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftTableButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private GameObject CraftTablePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CraftTablePanel.SetActive(true);
        }

    }
}
