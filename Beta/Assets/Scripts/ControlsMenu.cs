using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{

    public GameObject pauseMenuPanel;
    PauseMenu pauseMenu;

    public static bool isActive;

    void Start()
    {
        pauseMenu = pauseMenuPanel.GetComponent<PauseMenu>();
    }

    public void Back()
    {
        Hide();
        pauseMenu.ShowPause();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        isActive = true;
    }

    public void Hide()
    {
        // Deactivate the panel
        gameObject.SetActive(false);
        isActive = false;
    }
}