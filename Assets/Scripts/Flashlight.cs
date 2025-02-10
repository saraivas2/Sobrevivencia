using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flashlight : MonoBehaviour
{
    public GameObject lightGO;

    private bool isOn = false; 

     void Start()
    {
        lightGO.SetActive(isOn);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
