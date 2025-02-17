using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class BarraVidaPlayer : MonoBehaviour
{
    public GameObject barra;
    public Canvas canvas;
    MovimentPlayer ScriptDante;
    float barravidaFull;
    float vidafull;

    private void Start()
    {
        barravidaFull = barra.transform.localScale.x;
        ScriptDante = GameObject.Find("Breathing Idle").GetComponent<MovimentPlayer>();
        vidafull = ScriptDante.GetVida();
    }

    // Update is called once per frame
    void Update()
    {
        
        float barraVida = barra.transform.localScale.x;
        float vida = ScriptDante.GetVida();


        float barraAtual = (barravidaFull * vida) / vidafull;
        if (barraAtual < 0)
        {
            barraAtual = 0;
        }
        barra.transform.localScale = new Vector3(barraAtual, barra.transform.localScale.y, barra.transform.localScale.z);
    }
}
