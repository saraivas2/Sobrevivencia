using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoitLightSpider : MonoBehaviour
{

    [SerializeField] private Light pointlight;
    [SerializeField] private Transform Dante;
    [SerializeField] private Transform Spider;
    public float distanceSpider;
    bool lightOn;
    // Start is called before the first frame update
    void Start()
    {
        pointlight.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Dante.position, Spider.position);

        if (distance < distanceSpider)
        {
            pointlight.enabled = true;
            lightOn = true; 
        }
        else
        {
            pointlight.enabled = false;
            lightOn = false;
        }

        if (Spider.GetComponent<SpiderControlller>().GetVida()<=0)
        {
            pointlight.enabled = false;
        }
    }

    public bool lightOnOff()
    {
        return lightOn;
    }
}
