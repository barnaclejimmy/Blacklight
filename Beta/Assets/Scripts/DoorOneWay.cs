using UnityEngine;

public class DoorOneWay: MonoBehaviour
{
    private Transform door;
    private Door doorScript;

    private void Start()
    {
        door = transform.parent.GetChild(1);
        doorScript = door.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorScript.oneWay = true;
            doorScript.Shut();
            Destroy(gameObject);
        }
    }
}
