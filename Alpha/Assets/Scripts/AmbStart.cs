using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbStart : MonoBehaviour
{
    // Reference to Game Master script
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        // Make a new audio source and give it the spawn clip set in the
        // Game Master's inspector panel
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        AudioSource startSource = gameObject.AddComponent<AudioSource>();
        startSource.clip = gm.spawnAmbience;
        gm.currentAmbience = gm.spawnAmbience;
        startSource.Play();
    }
}