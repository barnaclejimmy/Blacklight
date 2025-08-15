using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{ 
    // Reference to UI panel that is our pause menu
    public GameObject pauseMenuPanel;
    public GameObject controlsMenuPanel;
    // Reference to panel's script object 
    PauseMenu pauseMenu;
    ControlsMenu controlsMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise the reference to the script object, which is a
        // component of the pause menu panel game object
        pauseMenu = pauseMenuPanel.GetComponent<PauseMenu>();
        controlsMenu = controlsMenuPanel.GetComponent<ControlsMenu>();
        pauseMenu.Hide();
        controlsMenu.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (((Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))) && !ControlsMenu.isActive)
        {
            // If user presses p, show the pause menu in pause mode
            pauseMenu.ShowPause();
        }
    }
}