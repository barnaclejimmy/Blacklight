using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Died : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            SceneManager.LoadScene("AmmoPickups");
        }
    }

    void OnGUI()
    {
        GUI.color = new Color(0.45f, 0f, 0f);
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.label.fontSize = 40;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        //GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 2), "You Died");
        GUI.Label(new Rect(0, 0, Screen.width, (Screen.height * 2 - 100)), "Press E to retry");
    }
}
