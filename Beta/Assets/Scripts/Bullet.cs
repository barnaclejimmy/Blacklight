using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        // Check if the game object is visible, if not, destroy self   
        if (!Utility.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
