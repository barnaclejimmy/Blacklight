using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [HideInInspector] public bool inRange;
    private float resetTime;
    private Vector2 originalPos, recentPos;
    public bool hitByLight;

    private void Awake()
    {
        originalPos = transform.parent.position;
        inRange = false;
        hitByLight = false;
    }

    /*private void Start()
    {
        transform.parent.gameObject.AddComponent<AStarPathfinder>();
        transform.parent.GetComponent<AStarPathfinder>().gridObject = GameObject.FindGameObjectWithTag("PathfindingGrid");
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !inRange)
        {
            inRange = true;
            transform.parent.gameObject.AddComponent<AStarPathfinder>();
            transform.parent.GetComponent<AStarPathfinder>().gridObject = GameObject.FindGameObjectWithTag("PathfindingGrid");
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
        else if (resetTime > 0)
        {
            resetTime -= Time.deltaTime;
        }
        else if (!hitByLight)
        {
            transform.parent.position = originalPos;
        }
        else
        {
            hitByLight = false;
        }
    }
}
