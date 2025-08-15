using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlinker : MonoBehaviour {

    // Which light will be blinking
    public GameObject lightPrefab;

    // Time spent on
    public float timeOn;

    // Time spent off
    public float timeOff;

    // Static variable for lights to reference
    public static float radius;

    // Radius of lights to initialise
    public float lightRadius = 0f;

    // Start is called before the first frame update
    void Start() {
        radius = lightRadius;
        StartCoroutine(Blinky());
    }

    // Creates and destroys the light in use
    IEnumerator Blinky() {
        while (true) {
            radius = lightRadius;
            GameObject light = Instantiate(lightPrefab, transform);
            yield return new WaitForSeconds(timeOn);
            Destroy(light);
            yield return new WaitForSeconds(timeOff);
        }
    }
}