using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 startPos;
    Quaternion startRot;
    //public static bool action;
    public GameObject crosshair;
    public AudioClip openSound;
    public AudioClip closeSound;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        startPos = transform.position;
        startRot = transform.rotation;
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseOver()
    {
        if (DoorOpen.inRange)
        {
            crosshair.GetComponent<CursorSprite>().Grab();
            if (Input.GetMouseButtonDown(1))
            {
                if (rb.isKinematic)
                {
                    audioSource.PlayOneShot(openSound);
                    rb.isKinematic = false;
                    if (DoorOpen.inLeft)
                    {
                        rb.AddForce((Vector2.right + Vector2.up) * 12, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rb.AddForce((Vector2.left + Vector2.down) * 12, ForceMode2D.Impulse);
                    }
                    gameObject.tag = "OpenDoor";
                }
                else
                {
                    audioSource.PlayOneShot(closeSound);
                    rb.isKinematic = true;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0;
                    transform.position = startPos;
                    transform.rotation = startRot;
                    gameObject.tag = "Wall";
                }
                //action = true;
            }
        }
    }

    private void OnMouseExit()
    {
        crosshair.GetComponent<CursorSprite>().UnGrab();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (transform.position.x - collision.transform.position.x < 0)
            {
                rb.AddTorque(1000);
            }
            else
            {
                rb.AddTorque(-1000);
            }
        }
    }
}
