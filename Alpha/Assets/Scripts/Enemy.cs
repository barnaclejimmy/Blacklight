using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private GameObject purp = null;
    public bool frozen = false;
    public int lives;
    private Chase ch;
    //private float movement;

    private void Start()
    {
        ch = gameObject.GetComponent<Chase>();
        //movement = ch.speed;
    }

    private void HitByUVRay(GameObject light)
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

    private void HitByRedRay(GameObject light)
    {
        purp = light;
        //if (movement < ch.baseSpeed/4) {
        frozen = true;
        //}
    }

    private void FixedUpdate()
    {
        if (player != null && purp == null)
        {
            // Get the Screen positions of the enemy
            Vector2 positionOnScreen = transform.position;
            // Get the Screen position of the player
            Vector2 playerPosition = player.position;
            // Get the angle between the points
            float angle = Mathf.Atan2(positionOnScreen.y - playerPosition.y, positionOnScreen.x - playerPosition.x) * Mathf.Rad2Deg;
            // Rotate enemy accordingly
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        }
        if (purp == null) {
            frozen = false;
        }
    }
}
