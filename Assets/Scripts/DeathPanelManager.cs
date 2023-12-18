using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelController : MonoBehaviour
{
    public int mainMenuSceneIndex = 0;
    public int gameSceneIndex = 1;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void RestartGame()
    {
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
