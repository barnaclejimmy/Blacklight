using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Reference to Game Master object
     GameMaster gameMaster;

    // Reference to the Player object's Rigidbody2D component
    Rigidbody2D body;

    // Current movement input
    Vector2 movement;

    // Movement speed of the player
    float movementSpeed = 4;

    // Multiplier to apply to diagonal movement
    float diagonalLimiter = 0.7f;


    // References the cursor object
    public Transform cursor;
    // Determine if the cursor should be allowed to move
    public static bool cursorMove = true;

    // Control player health and damage
    float health = 100f;
    float maxHealth;
    float damageRate = 1.5f;
    float healDelay = 1f;
    float healRate = 0.5f;
    float lastHitTime;

    // Reference to screen vignette object's SpriteRenderer
    SpriteRenderer vignette;

    // RBG red value of the vignette's colour
    float vignetteRed;

    // Reference to playerAudio script
    private PlayerAudio playerAudio;

    // Determine whether or not to draw health HUD text, set in inspector
    public bool drawHealthHUD;

    private void Awake()
    {
        // Initialise references to self object
        maxHealth = health;
        lastHitTime = Time.time;
        playerAudio = GetComponent<PlayerAudio>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // Move the player to the position of the last checkpoint they visited
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        transform.position = gameMaster.spawnPosition;

        // Set player ammo to what it was when they visted the last checkpoint
        Shoot shoot = GetComponent<Shoot>();
        shoot.ammo = gameMaster.spawnAmmo;

        // Get reference to the vignette object's SpriteRenderer
        vignette = GameObject.FindGameObjectWithTag("Vignette").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get movement input (each gets a value between -1 and 1)
        movement.x = Input.GetAxisRaw("Horizontal");  // -1 is left
        movement.y = Input.GetAxisRaw("Vertical");  // -1 is down

        // Reload scene if player health reaches zero
        if (health <= 0)
        {
            SceneManager.LoadScene("Died"/*SceneManager.GetActiveScene().buildIndex*/);
        }

        // Heal player health a short duration after stopped recieving damage
        if (health < maxHealth)
        {
            if (Time.time > lastHitTime + healDelay)
            {
                health += healRate;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
        }

        // Change RBG red value of the vignette's colour depending on player
        // health
        // 100% health = 0
        // 0% health = 0.5
        vignetteRed = (-1 * (health - 100)) / 200;
        vignette.color = new Color(vignetteRed, 0f, 0f, 1f);
    }

    private void FixedUpdate()
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
    }

    // Prevent cursor from moving when it rolls over the player
    private void OnMouseOver()
    {
        cursorMove = false;
    }

    private void OnMouseExit()
    {
        cursorMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
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
                shoot.ammo += 5;
                shoot.StartCoroutine("HUDRefillFlash");
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= damageRate;
        }
    }

    // Change background ambience audio loop when touching ambience triggers
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "AmbienceTrigger")
        {
            other.gameObject.GetComponent<AmbChange>().changeClip();
        }
    }

    // Draw health HUD text for debugging (enabled/disabled in inspector)
    private void OnGUI()
    {
        if (drawHealthHUD)
        {
            GUI.skin.label.alignment = TextAnchor.LowerLeft;
            GUI.color = Color.white;
            GUI.skin.label.fontSize = 30;
            GUI.Label(new Rect(15, Screen.height - 120, 500, 100), health + "%");
        }
    }
}
