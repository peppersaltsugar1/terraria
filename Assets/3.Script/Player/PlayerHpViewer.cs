using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpViewer : MonoBehaviour
{

    [SerializeField]private Text hpText;


    private void Update()
    {
        hpText.text = GameManager.Instance.playerControl.playerMaxHp.ToString() + "/" + GameManager.Instance.playerControl.playerCurrenHp.ToString();
    }
}
