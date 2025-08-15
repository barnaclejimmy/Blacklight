using UnityEngine;
using UnityEngine.SceneManagement;

public class Finale : MonoBehaviour
{
    float finish;
    float wait = 3f;

    void Start()
    {
        finish = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > finish + wait) {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.color = new Color(1f, 0.8f, 0.8f, 1f);
        GUI.skin.label.fontSize = 40;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect((Screen.width / 2) - 250, 200, 500, 500), "You have successfully escaped!");

        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.color = new Color(1f, 0.8f, 0.8f, 1f);
        GUI.skin.label.fontSize = 40;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 100, 300, 200), "Kills: " + Stats.kills);
        GUI.Label(new Rect((Screen.width / 2) + 100, (Screen.height / 2) - 100, 300, 200), "Deaths: " + Stats.deaths);
        GUI.Label(new Rect((Screen.width / 2) - 400, (Screen.height / 2) - 300, 300, 200), "Shots Fired: " + Stats.shotsFired);
        GUI.Label(new Rect((Screen.width / 2) + 100, (Screen.height / 2) - 300, 300, 200), "Shots Hit: " + Stats.shotsHit);

        GUI.skin.label.alignment = TextAnchor.LowerCenter;
        GUI.color = new Color(1f, 0.8f, 0.8f, 1f);
        GUI.skin.label.fontSize = 40;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect((Screen.width / 2) - 250, 700, 500, 500), "Press any key to return to the menu");
    }
}
