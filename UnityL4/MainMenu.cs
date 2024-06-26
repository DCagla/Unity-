using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;

    void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartGame()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        
        playerScript playerComponent = FindObjectOfType<playerScript>(); 
        if (playerComponent != null)
        {
            playerComponent.enabled = true;
            playerComponent.StartSpawningGhosts(); 
        }

        InGameMenu inGameMenuComponent = FindObjectOfType<InGameMenu>(); 
        if (inGameMenuComponent != null)
        {
            inGameMenuComponent.gameObject.SetActive(true);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            playerScript Player = FindObjectOfType<playerScript>();
            if (Player != null)
            {
                Player.StartSpawningGhosts();
            }
        }
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
