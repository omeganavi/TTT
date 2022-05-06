using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUI : MonoBehaviour
{

    public GameObject canvas;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (canvas.activeInHierarchy == true)
            {
                canvas.SetActive(false);
            }
            else
            {
                canvas.SetActive(true);
            }
        }
        
    }
}
