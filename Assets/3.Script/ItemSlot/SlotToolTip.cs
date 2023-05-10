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
        Vector3 offset = new Vector3(60f, -50f, 0f); // �̵��� ������ ���� �����մϴ�.
        transform.position = Input.mousePosition + offset; // ���콺 ��ġ�� ������ ���� ���� �̵��մϴ�.

        
    }
}
