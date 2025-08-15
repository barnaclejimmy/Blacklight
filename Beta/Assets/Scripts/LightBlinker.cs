using System.Collections;
using UnityEngine;

// Allows us to pick the colour of the light spawned
public enum eType
{
    Green, Purple, Red, Yellow
}

public class LightBlinker : MonoBehaviour
{
    // Which light will be blinking
    public GameObject lightPrefab;

    // Time spent on
    public float timeOn;

    // Time spent off
    public float timeOff;

    // Radius of lights
    public float radius;

    // Colour selection from eType
    public eType colour;

    public bool alwaysOn;

    // Start is called before the first frame update
    void Start()
    {
        if (alwaysOn == true)
        {
            GameObject light = Instantiate(lightPrefab, transform);
        }
        else {
            StartCoroutine(Blinky());
        }
    }

    // Creates and destroys the light in use
    IEnumerator Blinky()
    {
        while (true)
        {
            GameObject light = Instantiate(lightPrefab, transform);
            yield return new WaitForSeconds(timeOn);
            Destroy(light);
            yield return new WaitForSeconds(timeOff);
        }
    }
}
