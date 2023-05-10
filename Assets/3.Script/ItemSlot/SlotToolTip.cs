using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDesc;

    public void ShowToolTip(Item _item)
    {
        toolTip.SetActive(true);
        itemName.text = _item.name;
        itemDesc.text = _item.itemDesc;
    }
    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    private void Update()
    {
        Vector3 offset = new Vector3(60f, -50f, 0f); // 이동할 오프셋 값을 지정합니다.
        transform.position = Input.mousePosition + offset; // 마우스 위치에 오프셋 값을 더해 이동합니다.

        
    }
}
