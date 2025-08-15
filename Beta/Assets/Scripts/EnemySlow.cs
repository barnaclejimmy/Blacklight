using UnityEngine;

public class EnemySlow : MonoBehaviour
{
    private Chase chase;

    private void Start()
    {
        chase = transform.parent.GetComponentInParent<Chase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            chase.baseSpeed /= 8;
            Stats.shotsHit++;
        }
    }
}
