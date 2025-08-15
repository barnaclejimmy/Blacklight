using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    private Transform door;
    private Door doorScript;
    private Crosshair crosshairScript;
    
    private void Start()
    {
        door = transform.parent.GetChild(1);
        doorScript = door.GetComponent<Door>();
        crosshairScript = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
    }

    private void Update()
    {
        transform.position = new Vector2(door.position.x, door.position.y);
        transform.rotation = door.rotation;
    }

    private void OnMouseOver()
    {
        if (DoorOpen.inRange)
        {
            crosshairScript.Grab();
            if (Input.GetMouseButtonDown(1))
            {
                doorScript.Open();
            }
        }
    }

    private void OnMouseExit()
    {
        crosshairScript.UnGrab();
    }
}
