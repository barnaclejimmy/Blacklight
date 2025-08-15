using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoot, noAmmo, reload;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(shoot);
    }

    public void NoAmmo()
    {
        audioSource.PlayOneShot(noAmmo);
    }

    public void Reload()
    {
        audioSource.PlayOneShot(reload);
    }
}
