using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound, noAmmoSound, reloadSound, deathSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
    }

    public void NoAmmo()
    {
        audioSource.PlayOneShot(noAmmoSound);
    }

    public void Reload()
    {
        audioSource.PlayOneShot(reloadSound);
    }

    public void Death()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
