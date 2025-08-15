using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Indicates whether the game in paused mode
    public static bool pauseGame;

    public GameObject controlsMenuPanel;
    ControlsMenu controlsMenu;

    public AudioMixer audioMixer;

    void Start()
    {
        controlsMenu = controlsMenuPanel.GetComponent<ControlsMenu>();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    // Show the pause menu in pause mode (the
    // first option will say "Resume")
    public void ShowPause()
    {
        // Pause the game
        pauseGame = true;
        // Show the panel
        gameObject.SetActive(true);
    }

    // Hide the menu panel
    public void Hide()
    {
        // Deactivate the panel
        gameObject.SetActive(false);
        // Resume the game (if paused)
        pauseGame = false;
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        Hide();
    }

    public void Controls()
    {
        gameObject.SetActive(false);
        controlsMenu.Show();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseGame)
        {
            Time.timeScale = 0;
        }
    }
}