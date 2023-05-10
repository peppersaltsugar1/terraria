using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingPanel : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            transform.gameObject.SetActive(false);
        }
    }
}
