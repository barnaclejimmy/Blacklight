using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Amount of ammo the player currently has
    // Note: the amount of ammo the player spawns with is controlled by the
    // Game Master. To set the amount of ammo the player starts with, use the
    // Game Master's inspector panel.
    [HideInInspector] public int ammo;

    // Maximum ammo the player can have, set in inspector panel
    public int magazineSize;

    // Reference to shot origin object, which is a child of Player, set in
    // inspector panel
    public Transform firePoint;

    // Reference to bullet object prefab, set in inspector panel
    public GameObject shotPrefab;

    // Force of the shot
    int shotForce = 40;

    // Duration of shot cooldown
    float cooldownTime = 0.3f;

    // Current state of cooldown
    float cooldown;

    // Duration of screen shake
    float screenShakeTime = 0.05f;

    // Current state of screen shake
    // This will be used by the CameraMovement script.
    public static float screenShake;

    // Colour of the HUD text displaying current ammo
    Color ammoHUDColour = new Color(1f, 1f, 1f, 0.5f); // White at 50% opacity

    // Texture used for symbol on ammo count HUD, set in inspector panel
    public Texture ammoHUDSymbol;

    // Reference to the player object's PlayerAudio component
    private PlayerAudio playerAudio;

    void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        // Limit the amount of ammo collecetd
        if (ammo > magazineSize)
        {
            ammo = magazineSize;
        }

        // Change ammo text colour based on current amount
        if (ammo == 4 || ammo == 3)
        {
            // Yellow
            ammoHUDColour.g = 0.92f;
            ammoHUDColour.b = 0.016f;
        }
        else if (ammo == 2 || ammo == 1)
        {
            // Orange
            ammoHUDColour.g = 0.46f;
        }
        else if (ammo == 0)
        {
            // Red
            ammoHUDColour.g = 0f;
            ammoHUDColour.b = 0f;
        }

        // Main shoot effect
        if (cooldown <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                if (ammo > 0)
                {
                    playerAudio.Shoot();
                    GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
                    // Set screen shake to maximum duration
                    screenShake = screenShakeTime;
                    ammo--;
                    if (ammo == 0)
                    {
                        StartCoroutine("HUDEmptyFlash");
                    }
                }
                else
                {
                    playerAudio.NoAmmo();
                    StartCoroutine("HUDEmptyFlash");
                }
                // Set cooldown time no matter what
                cooldown = cooldownTime;
            }
        }
        else
        {
            // Decrease duration of cooldown over time
            cooldown -= Time.deltaTime;
        }
    }

    // Draw ammo HUD text
    void OnGUI()
    {
        // Bullet symbol
        GUI.skin.label.alignment = TextAnchor.MiddleRight;
        GUI.color = ammoHUDColour;
        GUI.DrawTexture(new Rect(Screen.width - 457, Screen.height - 57, 500, 20), ammoHUDSymbol, ScaleMode.ScaleToFit);

        // Currently held ammo
        GUI.skin.label.alignment = TextAnchor.LowerRight;
        GUI.skin.label.fontSize = 50;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect(Screen.width - 605, Screen.height - 115, 500, 100), ammo.ToString());

        // Magazine size;
        GUI.color = new Color(1f, 1f, 1f, 0.5f); // White at 50% opacity
        GUI.skin.label.fontSize = 30;
        GUI.skin.label.fontStyle = FontStyle.Normal;
        GUI.Label(new Rect(Screen.width - 540, Screen.height - 120, 500, 100), " / " + magazineSize);
       
    }

    // When player refills ammo, change HUD text colour to white and flash it
    // at full opacity
    IEnumerator HUDRefillFlash()
    {
        ammoHUDColour = new Color(1f, 1f, 1f, 1f); // White at 100% opacity
        yield return new WaitForSeconds(0.3f);
        ammoHUDColour.a = 0.5f; // 50% opacity
        yield break;
    }

    // When player runs out of ammo or shoots without ammo, flash HUD text at
    // full opacity (it should be red by now)
    IEnumerator HUDEmptyFlash()
    {
        ammoHUDColour.a = 1f; // % opacity
        yield return new WaitForSeconds(0.1f);
        ammoHUDColour.a = 0.5f; // 50% opacity
        yield break;
    }
}