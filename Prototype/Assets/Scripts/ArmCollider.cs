using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Player") {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/
    }
}
