using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    private static GameMaster instance;

    // Position of the last checkpoint the player visited
    // This will be used by the Player script to position the Player object
    public Vector2 spawnPosition;

    // Amount of ammo the player had at the last checkpoint
    // Note: do not set this to a value lower than 5 or the colour of the ammo
    // counter HUD will whack out 
    public int spawnAmmo;

    // The currently playing ambience audio clip
    [HideInInspector] public AudioClip currentAmbience;

    // Ambience clip that was playing at the last checkpoint
    public AudioClip spawnAmbience;

    // Stop the Game Master data fields from losing their values when a scene
    // reloads
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}