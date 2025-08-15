using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Reference to the position of the player object, set in inspector
    public Transform player;

    // Reference to screen fade object's SpriteRenderer
    SpriteRenderer fade;

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


    // Start is called before the first frame update
    private void Start()
    {
        // Get reference to the fade object's SpriteRenderer
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<SpriteRenderer>();
        fade.color = Color.black;
        // Fade the scene in by fading out the fade object
        StartCoroutine("sceneFadeIn");
    }

    // Scene fade in coroutine
    IEnumerator sceneFadeIn()
    {
        Color fadeColour = Color.black;
        float fadeAlpha = 1f;

        while (fade.color.a > 0) {
            fadeAlpha -= 0.01f;
            fade.color = new Color(0f, 0f, 0f, fadeAlpha);
            yield return null;
        }
        yield break;
    }
}