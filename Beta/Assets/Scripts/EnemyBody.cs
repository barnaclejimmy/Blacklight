using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet") && enemy.frozen == true)
        {
            Destroy(collision.gameObject);
            enemy.lives--;
            if (enemy.lives == 0)
            {
                Stats.kills++;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
