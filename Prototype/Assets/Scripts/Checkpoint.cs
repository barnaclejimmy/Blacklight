using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Reference to Game Master script
    private GameMaster gm;

    // Reference to player's Shoot script
    private Shoot shoot;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
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
        }
    }
}