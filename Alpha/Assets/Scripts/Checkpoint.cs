using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Reference to Game Master script
    GameMaster gm;

    // Reference to player's Shoot script
    Shoot shoot;

    // Determine if the game has just been saved
    bool gameSaved;

    // Duration to display "GAME SAVED" HUD text
    float gameSavedDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
    }

    // If checkpoint collides with player, tell Game Master the checkpoint
    // position, amount of ammo the player has, and current ambience clip
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.spawnPosition = transform.position;
            gm.spawnAmmo = shoot.ammo;
            gm.spawnAmbience = gm.currentAmbience;
            if (Time.timeSinceLevelLoad > 1)
            {
                StartCoroutine("showGameSaved");
            }
        }
    }

    // Draw HUD text for game save
    void OnGUI()
    {
        if (gameSaved)
        {
            GUI.skin.label.alignment = TextAnchor.LowerCenter;
            GUI.color = Color.white;
            GUI.skin.label.fontSize = 30;
            GUI.Label(new Rect((Screen.width / 2) - 250, Screen.height - 120, 500, 100), "GAME SAVED.");
        }
    }

    // Show "GAME SAVED" HUD text, wait for duration, then hide
    IEnumerator showGameSaved()
    {
        gameSaved = true;
        yield return new WaitForSeconds(gameSavedDuration);
        gameSaved = false;
        yield break;
    }
}