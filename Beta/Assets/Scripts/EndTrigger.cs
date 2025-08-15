using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{

    public static bool key;
    public static bool end;

    // Start is called before the first frame update
    void Start()
    {
        key = false;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (key && end)
        {
            SceneManager.LoadScene("Finale");
        }
    }
}
