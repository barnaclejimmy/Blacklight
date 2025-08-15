using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Reference to the position of the player object
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        // Set position to the position of the player on the x and y axis
        // Force z position to -10
        transform.position = new Vector3(player.position.x, player.position.y, -10);

        // If the shoot script changes the value of screen shake duration,
        // reposition the camera randomly inside a sphere of given radius
        if (Shoot.screenShake > 0)
        {
            transform.position += Random.insideUnitSphere * 0.06f;
            // Decrease duration of screen shake over time
            Shoot.screenShake -= Time.deltaTime;
        }
    }
}