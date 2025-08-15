using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public bool inRange;
    private float resetTime;
    private Vector2 originalPos;
    private Vector2 recentPos;

    private void Awake()
    {
        originalPos = transform.parent.position;
        inRange = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            transform.parent.gameObject.AddComponent<AStarPathfinder>();
            transform.parent.gameObject.GetComponent<AStarPathfinder>().gridObject = GameObject.Find("Collision Grid");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            Destroy(GetComponentInParent<AStarPathfinder>());
            resetTime = 3;
        }
    }

    private void LateUpdate()
    {
        if (recentPos != (Vector2)transform.position)
        {
            recentPos = transform.position;
            resetTime = 3;
        }
        else
        {
            if (resetTime > 0)
            {
                resetTime -= Time.deltaTime;
            }
            else
            {
                transform.parent.position = originalPos;
            }
        }
    }
}
