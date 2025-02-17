using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraVida : MonoBehaviour
{

    public GameObject spider;
    public GameObject Dante;
    public TextMesh barra;
    public float vidaFull;
    public float vida;
    public Camera cam;
    public float distanceSpider, distance;

    void Update()
    {
        Vector3 dantepos = Dante.transform.position;
        Vector3 spiderpos = spider.transform.position;
        distance = Vector3.Distance(dantepos,spiderpos);

        if (distance < distanceSpider)
        {
            barra.gameObject.SetActive(true);

            if (spider == null) return;

            SpiderControlller spiderScript = spider.GetComponent<SpiderControlller>();

            if (spiderScript == null) return;

            float vidaAtual = spiderScript.GetVida();
            vidaFull = spiderScript.GetVidaFull();

            vida = (vidaAtual / vidaFull) * 100;

            barra.text = vida.ToString("F0");
            barra.color = Color.white;
        }
        else
        {
            barra.gameObject.SetActive(false);
        }

        
    }

}
