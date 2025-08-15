using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{ 
    // Reference to UI panel that is our pause menu
    public GameObject pauseMenuPanel;
    // Reference to panel's script object 
    PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise the reference to the script object, which is a
        // component of the pause menu panel game object
        pauseMenu = pauseMenuPanel.GetComponent<PauseMenu>();
        pauseMenu.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            // If user presses p, show the pause menu in pause mode
            pauseMenu.ShowPause();
        }
    }
}
