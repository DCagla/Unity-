using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenuCanvas;

    void Start()
    {
        inGameMenuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("12");
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        bool isPaused = inGameMenuCanvas.activeSelf;
        inGameMenuCanvas.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1 : 0; 
    }

    public void ResumeGame()
    {
        inGameMenuCanvas.SetActive(false);
        Time.timeScale = 1; 
    }

    public void ExitGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
