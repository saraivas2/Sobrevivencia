using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private string gameScene1;
    public void Jogar()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(gameScene1);
    }
}
