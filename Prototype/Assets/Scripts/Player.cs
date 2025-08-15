using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Reference to Game Master object
    private GameMaster gm;

    // Reference to the Player object's Rigidbody2D component
    Rigidbody2D body;

    // Current movement input
    Vector2 movement;

    // Movement speed of the player
    float movementSpeed = 4;

    // Multiplier to apply to diagonal movement
    float diagonalLimiter = 0.7f;

    // Crosshair detector
    public static bool shouldMove = true;

    // References the cursor object
    public Transform cursor;

    // Health and damage taken
    public float health = 100f;
    private float fullHealth;
    public float damageAmount = 7f;
    public float healAmount = 1f;

    public float repeatDamagePeriod = 0.3f;
    public float timeToHeal = 10f;
    private float lastHitTime;

    private PlayerAudio playerAudio;

    void Awake()
    {
        fullHealth = health;
        lastHitTime = Time.time;
        playerAudio = GetComponent<PlayerAudio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // Move the player to the position of the last checkpoint they visited
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.spawnPosition;

        // Set player ammo to what it was when they visted the last checkpoint
        Shoot shoot = GetComponent<Shoot>();
        shoot.ammo = gm.spawnAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        // Gives a value between -1 and 1
        movement.x = Input.GetAxisRaw("Horizontal");  // -1 is left
        movement.y = Input.GetAxisRaw("Vertical");  // -1 is down
    }

    void FixedUpdate()
    {
        // Get world position of the player
        Vector2 worldPosition = transform.position;

        // Get world position of the mouse
        //Vector2 mouseWorldPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get world position of the cursor
        Vector2 mouseWorldPosition = cursor.position;

        // Get the angle between the player and mouse positions
        float angle = Mathf.Atan2(worldPosition.y - mouseWorldPosition.y, worldPosition.x - mouseWorldPosition.x) * Mathf.Rad2Deg;

        // Rotate the player accordingly
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));

        // Check for diagonal movement and limit movement speed diagonally
        if (movement.x != 0 && movement.y != 0) 
        {
            movement.x *= diagonalLimiter;
            movement.y *= diagonalLimiter;
        }

        // Apply velocity to the player
        body.velocity = new Vector2(movement.x * movementSpeed, movement.y * movementSpeed);

        if (health <= 0)
        {
            GetComponent<PlayerAudio>().Death();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (health < fullHealth)
        {
            if (Time.time > lastHitTime + timeToHeal)
            {
                health += healAmount;
                if (health > fullHealth)
                {
                    health = fullHealth;
                }
            }
        }
    }

    // Prevents cursor from moving over player
    void OnMouseOver()
    {
        shouldMove = false;
    }

    void OnMouseExit()
    {
        shouldMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Refill the player's ammo if they touch an ammo crate
        if (other.CompareTag("AmmoCrate"))
        {
            Shoot shoot = GetComponent<Shoot>();
            if (shoot.ammo != shoot.magazineSize)
            {
                playerAudio.Reload();
                shoot.ammo = shoot.magazineSize;
                shoot.StartCoroutine("HUDRefillFlash");
            }
        }

        // Add 5 to the player's ammo if they touch an ammo pickup, then
        // destroy the pickup
        if (other.CompareTag("AmmoPickup"))
        {
            Shoot shoot = GetComponent<Shoot>();
            if (shoot.ammo != shoot.magazineSize)
            {
                playerAudio.Reload();
                //shoot.audioSource.PlayOneShot(shoot.reloadSound);
                shoot.ammo += 5;
                shoot.StartCoroutine("HUDRefillFlash");
                Destroy(other.gameObject);
            }
        }
    }

    // Deals damage on contact with an enemy
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (health > 0) {
                if (Time.time > lastHitTime + repeatDamagePeriod)
                {
                    health -= damageAmount;
                    lastHitTime = Time.time;
                }
            }
        }

        // Refill the player's ammo to maximum if they touch an ammo crate
        if (other.CompareTag("AmmoCrate"))
        {
            Shoot shoot = GetComponent<Shoot>();
            shoot.ammo = shoot.magazineSize;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "AmbienceTrigger")
        {
            other.gameObject.GetComponent<AmbChange>().changeClip();
        }
    }
}
