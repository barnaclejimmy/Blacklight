using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    public Transform player;

    private GameObject purp = null;

    void HitByUVRay(GameObject light)
    {
        purp = light;
        // Get the Screen positions of the enemy
        Vector2 positionOnScreen = transform.position;
        // Get the Screen position of the player
        Vector2 playerPosition = light.transform.position;
        // Get the angle between the points
        float angle = Mathf.Atan2(positionOnScreen.y - playerPosition.y, positionOnScreen.x - playerPosition.x) * Mathf.Rad2Deg;
        // Rotate enemy accordingly
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 270));
    }

    void FixedUpdate() {
        if (player != null && purp == null) {
            // Get the Screen positions of the enemy
            Vector2 positionOnScreen = transform.position;
            // Get the Screen position of the player
            Vector2 playerPosition = player.position;
            // Get the angle between the points
            float angle = Mathf.Atan2(positionOnScreen.y - playerPosition.y, positionOnScreen.x - playerPosition.x) * Mathf.Rad2Deg;
            // Rotate enemy accordingly
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        }
    }

    void OnTriggerEnter2D(Collider2D collision) { 
        if (collision.CompareTag("Bullet")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } /*else if (collision.CompareTag("Player")) {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/
    }
}
