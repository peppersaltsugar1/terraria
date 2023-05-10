using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmeltingButton : MonoBehaviour,IPointerClickHandler
{

    [SerializeField] private GameObject SmeltingPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SmeltingPanel.SetActive(true);
        }
    
    }
}
