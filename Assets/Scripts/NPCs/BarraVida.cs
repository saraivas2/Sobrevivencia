using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraVida : MonoBehaviour
{

    public GameObject spider;
    public TextMesh barra;
    public float vidaFull;
    public float vida;
    public Camera cam;

    void Update()
    {

        if (spider == null) return;

        SpiderControlller spiderScript = spider.GetComponent<SpiderControlller>();
        PoitLightSpider lightScript = spider.GetComponentInChildren<PoitLightSpider>();

        if (spiderScript == null) return;

        float vidaAtual = spiderScript.GetVida();
        vidaFull = spiderScript.GetVidaFull();

        vida = (vidaAtual / vidaFull) * 100;

        barra.text = vida.ToString("F0");
        barra.color = Color.white;

        if (vida <= 0)
        {
            barra.text = "";
        }

        if (lightScript.lightOnOff())
        {
            transform.rotation = cam.transform.rotation;
        }
    }

}
