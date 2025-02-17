using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject lightGO; 
    private bool isOn = false; 

     void Start()
    {
        lightGO.SetActive(isOn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isOn = !isOn;
           
            if (isOn)
            {
                lightGO.SetActive(true);
            }
            
            else
            {
                lightGO.SetActive(false);

            }
        }
    }
}
