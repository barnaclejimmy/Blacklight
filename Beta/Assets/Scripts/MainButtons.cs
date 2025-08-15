using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    // Reference to Game Master script
    GameMaster gm;

    public GameObject mainControlsPanel;
    public GameObject mainStatsPanel;
    MainControls mainControls;
    MainStats mainStats;

    public void Start()
    {
        mainStats = mainStatsPanel.GetComponent<MainStats>();
        mainControls = mainControlsPanel.GetComponent<MainControls>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    public void StartNewGame()
    {
        if (GameMaster.instance != null)
        {
            //gm.spawnPosition = new Vector2(0, 0);
            //gm.spawnAmmo = 5;
            gm.spawnAmbience = null;
            gm.firstSpawn = true;
        }
        Stats.deaths = 0;
        Stats.kills = 0;
        Stats.shotsFired = 0;
        Stats.shotsHit = 0;
        // Load the Level
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Beta");
    }

    public void Resume()
    {
        // Load the Level
        gm.firstSpawn = true;
        SceneManager.LoadScene("Beta");
    }

    public void Controls()
    {
        Hide();
        mainControls.Show();
    }

    public void Statistics()
    {
        Hide();
        mainStats.Show();
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    public void Hide()
    {
        // Deactivate the panel
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // Activate the panel
        gameObject.SetActive(true);
    }
}
