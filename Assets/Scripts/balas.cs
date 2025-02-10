using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class balas : MonoBehaviour
{
    public float vel;
    [SerializeField] private GameObject fire;

    void Update()
    {
        transform.Translate(new Vector2(vel * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("spider"))
        {
            SpiderControlller scriptSpider =  gameObject.GetComponent<SpiderControlller>();
            scriptSpider.VidaSpider(20);
        } else if (other.gameObject.CompareTag("wolf"))
        {
            SpiderControlller scriptSpider = gameObject.GetComponent<SpiderControlller>();
            scriptSpider.VidaSpider(15);
        }
        else if (other.gameObject.CompareTag("monster"))
        {
            SpiderControlller scriptSpider = gameObject.GetComponent<SpiderControlller>();
            scriptSpider.VidaSpider(20);
        }
    }

}
