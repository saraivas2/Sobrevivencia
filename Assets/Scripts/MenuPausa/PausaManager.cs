using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour
{
    [SerializeField] public GameObject PausePanel;
    [SerializeField] public GameObject PauseButton;
    [SerializeField] private string menuScene;

    public void Pausar()
    {
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Retomar()
    {
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void SairPMenu()
    {
        SceneManager.LoadScene(menuScene);
        Time.timeScale = 1;
    }
}
