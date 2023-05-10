using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    private Text viewLevel;
    private void Awake()
    {
        viewLevel = transform.gameObject.GetComponent<Text>();
    }

    public void PlayerLevelView()
    {
        viewLevel.text = "PlayerLevel : " + GameManager.Instance.playerControl.weaponLevel.ToString();
    }
}
