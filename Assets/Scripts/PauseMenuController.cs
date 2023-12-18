using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public int mainMenuSceneIndex = 0;
    public int gameSceneIndex = 1;
    public KeyCode pauseKey = KeyCode.Escape; 

    private PlayerController playerController;
    private bool isPaused = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        TogglePauseMenu(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu(!isPaused);
        }
    }

    private void TogglePauseMenu(bool show)
    {
        isPaused = show;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f; 
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    public void ContinueGame()
    {
        TogglePauseMenu(false);
    }

    public void RestartGame()
    {
        TogglePauseMenu(false);

        if (playerController != null)
        {
            playerController.AddLives(3);
        }

        SceneManager.LoadScene(gameSceneIndex);

        if (playerController != null)
        {
            playerController.Respawn();
        }
    }

    public void GoToMainMenu()
    {
        TogglePauseMenu(false);

        if (playerController != null)
        {
            playerController.AddLives(3);
        }

        SceneManager.LoadScene(mainMenuSceneIndex);

        if (playerController != null)
        {
            playerController.Respawn();
        }
    }
}
