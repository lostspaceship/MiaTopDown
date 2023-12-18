using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class NewBehaviourScript : MonoBehaviour
{
    public int mainMenuSceneIndex = 0;

    private PlayerController playerController;

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void playTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }

    public void MainMenu()
    {
        Key.collectedKeys = 0;

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
