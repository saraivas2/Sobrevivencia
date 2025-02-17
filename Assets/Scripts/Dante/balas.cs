using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class balas : MonoBehaviour
{
    public float vel;
    public GameObject fire;
    float timeLife = 5;
    private Vector3 direcao;
    public int damage;

    void Start()
    {
        direcao = transform.forward;
    }

    void Update()
    {
        transform.position += direcao * vel * Time.deltaTime;

        timeLife -= Time.deltaTime;
        if (timeLife <= 0)
        {
            Destroy(gameObject);
        }

    }

    public int Damage()
    {
        return damage;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direcao = newDirection;
        transform.forward = newDirection; 
    }

}
