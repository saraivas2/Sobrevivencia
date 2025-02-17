using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private Transform PontoFire;
    public Camera camera;
    
       
    public void InstantiateBala()
    {

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; 
        }
        else
        {
            targetPoint = ray.GetPoint(100f); 
        }

        // Calcula a direção normalizada da bala
        Vector3 direction = (targetPoint - PontoFire.position).normalized;

        GameObject balas = Instantiate(bala, PontoFire.position, Quaternion.identity);
        balas.GetComponent<balas>().SetDirection(direction);
    }
}
