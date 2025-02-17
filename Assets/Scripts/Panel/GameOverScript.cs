using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public Canvas canvas;
    public float time = 1.0f;
    public bool show = false;
    
    void Start()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }
    
    public void ShowTelaGameOver(bool show)
    {
        if (show)
        {
            show = true;
            time -= Time.deltaTime;
            
            canvas.GetComponent<Canvas>().enabled = true;

            if (time <= 0)
            {
             
                canvas.GetComponent<Canvas>().enabled = false;
                time = 1.0f;
                show = false;
            }
        }
       
    }
}
