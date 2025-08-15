using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public static bool inRange = false;
    public static bool inLeft = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        if (gameObject.name == "DoorOpenL")
        {
            inLeft = true;
        }
        else
        {
            inLeft = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
