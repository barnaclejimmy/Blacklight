using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainControlsPanel;
    public GameObject mainStatsPanel;
    public GameObject mainButtonsPanel;

    MainButtons mainButtons;
    MainControls mainControls;
    MainStats mainStats;

    void Start()
    {
        mainControls = mainControlsPanel.GetComponent<MainControls>();
        mainButtons = mainButtonsPanel.GetComponent<MainButtons>();
        mainStats = mainStatsPanel.GetComponent<MainStats>();
        mainControls.Hide();
        mainStats.Hide();
        mainButtons.Show();
    }
}