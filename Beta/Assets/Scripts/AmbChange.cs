using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

public class AmbChange : MonoBehaviour
{
    // Reference to Game Master script
    private GameMaster gm;

    // Audio clip to change to, set in inspector panel
    public AudioClip newClip;

    // Array to contain all already-playing audio sources in parent object
    AudioSource[] audioSources;

    //public AudioMixerGroup audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    public void changeClip()
    {
        // Make sure the clip to change to isn't the one that's already playing
        if (newClip != gm.currentAmbience)
        {
            // Tell Game Master what the new clip is
            gm.currentAmbience = newClip;

            // Get all the already-playing audio sources in parent object
            audioSources = gameObject.GetComponentsInParent<AudioSource>();

            // Fade out the already-playing audio sources
            foreach (AudioSource oldSource in audioSources)
            {
                // Don't worry about sources that are already muted
                if (oldSource.volume > 0)
                {
                    // Initialise fade out coroutine
                    StartCoroutine(sourceFadeOut(oldSource));
                }
            }

            // Make a new audio source and give it the new clip
            AudioSource newSource = transform.parent.gameObject.AddComponent<AudioSource>();
            //newSource.outputAudioMixerGroup = audioMixer;
            newSource.clip = newClip;
            newSource.loop = true;

            // Initialise fade in coroutine
            StartCoroutine(sourceFadeIn(newSource));
        }
    }

    // Fade in coroutine
    IEnumerator sourceFadeIn(AudioSource source)
    {
        float currentTime = 0;
        source.volume = 0;
        source.Play();
        while (currentTime < 0.4)
        {
            currentTime += Time.deltaTime;
            source.volume += 0.035f;
            yield return null;
        }
        yield break;
    }

    // Fade out coroutine
    IEnumerator sourceFadeOut(AudioSource source)
    {
        float currentTime = 0;
        while (currentTime < 0.4)
        {
            currentTime += Time.deltaTime;
            source.volume -= 0.04f;
            yield return null;

        }
        Destroy(source, 0.5f);
        yield break;
    }
}
