using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public static bool inRange = false, inLeft = false;
    private Crosshair crosshairScript;
    [SerializeField] private bool finalDoor = false;

    private void Start()
    {
        crosshairScript = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            if (name == "OpenL")
            {
                inLeft = true;
            }
            else
            {
                inLeft = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            crosshairScript.UnGrab();
            if (finalDoor)
            {
                EndTrigger.end = true;
            }
        }
    }
}
