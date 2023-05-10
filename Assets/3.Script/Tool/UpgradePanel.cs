using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            transform.gameObject.SetActive(false);
        }
    }
}
