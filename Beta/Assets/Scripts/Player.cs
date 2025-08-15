using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
    float healRate = 0.3f;
    float lastHitTime;

    // Reference to screen vignette object's SpriteRenderer
    SpriteRenderer vignette;

    // RBG red value of the vignette's colour
    float vignetteRed;

    // References to the scene's post-processing volume and its effects
    PostProcessVolume postProcessing;
    ChromaticAberration chromaticAberration;
    Bloom bloom;

    // Reference to playerAudio script
    private PlayerAudio playerAudio;

    // Determine whether or not to draw health HUD text, set in inspector
    public bool drawHealthHUD;

    // Audio source that will play the low health loop
    AudioSource lowHealthSource;

    // Audio clip for low health loop, set in inspector panel
    public AudioClip lowHealthLoopClip;

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
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
        }
        else
        {
            transform.position = new Vector3(43, -5.5f);
        }
        //transform.position = gameMaster.spawnPosition;

        // Set player ammo to what it was when they visted the last checkpoint
        // for some cursed reason this never works in the WebGL build no matter what approach we take
        // despite all methods working perfectly fine in the unity editor
        // so uhhhh 5 bullets will do
        Shoot shoot = GetComponent<Shoot>();
        /*if (PlayerPrefs.HasKey("a"))
        {
            shoot.ammo = PlayerPrefs.GetInt("a");
        }
        else
        {
            shoot.ammo = 5;
        }*/
        //shoot.ammo = gameMaster.spawnAmmo;
        //shoot.ammo = Shoot.checkpointAmmo;
        shoot.ammo = 5;

        // Get reference to the vignette object's SpriteRenderer
        vignette = GameObject.FindGameObjectWithTag("Vignette").GetComponent<SpriteRenderer>();

        // Get references to the scene's post-processing volume and its effects
        postProcessing = GameObject.FindGameObjectWithTag("Post-processing").GetComponent<PostProcessVolume>();
        postProcessing.profile.TryGetSettings(out chromaticAberration);
        postProcessing.profile.TryGetSettings(out bloom);

        // Make a new audio source to play the low health loop clip
        lowHealthSource = gameObject.AddComponent<AudioSource>();
        lowHealthSource.clip = lowHealthLoopClip;
        lowHealthSource.loop = true;
        lowHealthSource.volume = 0;
        lowHealthSource.Play();

        // Determine whether to play the death sound depending on if this is
        // the first time the player has spawned
        if (! gameMaster.firstSpawn) {
            playerAudio.Death();
        } else {
            gameMaster.firstSpawn = false;
        }
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
            Stats.deaths++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        vignetteRed = (health / -200) + 0.5f;
        vignette.color = new Color(vignetteRed, 0f, 0f, 1f);

        // Change chromatic aberration intensity depending on player health
        // 100% health = 0.2
        // 0% health = 1
        // (health / -125) + 1
        //
        // 100% health = 0.1
        // 0% health = 1
        chromaticAberration.intensity.Override((health / -(1000 / 9)) + 1);

        // Change bloom dirtiness intensity depending on player health
        // 100% health = 0
        // 0% health = 1
        bloom.dirtIntensity.Override((health / -100) + 1);

        // Change low health loop volume depending on player health
        // 100% health = 0
        // 0% health = 1
        lowHealthSource.volume = (health / -100) + 1;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Refill the player's ammo if they touch an ammo crate
        if (other.collider.CompareTag("AmmoCrate"))
        {
            Shoot shoot = GetComponent<Shoot>();
            if (shoot.ammo != shoot.magazineSize)
            {
                playerAudio.Reload();
                shoot.ammo = shoot.magazineSize;
                shoot.StartCoroutine("HUDRefillFlash");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
            lastHitTime = Time.time;
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
