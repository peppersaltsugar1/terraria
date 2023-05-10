using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTablePanel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            transform.gameObject.SetActive(false);
        }
    }
}
