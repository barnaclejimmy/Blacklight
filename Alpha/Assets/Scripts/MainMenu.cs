using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the "Level" scene
        SceneManager.LoadScene("AmmoPickups");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
