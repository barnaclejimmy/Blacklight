using UnityEngine;

public class DoorLock : MonoBehaviour
{
    private Door doorScript;

    private void Start()
    {
        doorScript = transform.parent.GetChild(1).GetComponent<Door>();
        doorScript.keyRequired = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("key obtained");
            doorScript.keyObtained = true;
            Destroy(gameObject);
        }
    }            
}
