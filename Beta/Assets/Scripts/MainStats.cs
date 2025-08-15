using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainStats : MonoBehaviour
{
    public GameObject mainButtonsPanel;
    MainButtons mainButtons;

    // Start is called before the first frame update
    void Start()
    {
        mainButtons = mainButtonsPanel.GetComponent<MainButtons>();
    }

    public void Back()
    {
        Hide();
        mainButtons.Show();
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

    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.color = new Color(1f, 0.8f, 0.8f, 1f);
        GUI.skin.label.fontSize = 40;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 300, 200), "Kills: " + Stats.kills);
        GUI.Label(new Rect((Screen.width / 2) + 100, (Screen.height / 2) - 100, 300, 200), "Deaths: " + Stats.deaths);
        GUI.Label(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 300, 300, 200), "Shots Fired: " + Stats.shotsFired);
        GUI.Label(new Rect((Screen.width / 2) + 100, (Screen.height / 2) - 300, 300, 200), "Shots Hit: " + Stats.shotsHit);
    }
}