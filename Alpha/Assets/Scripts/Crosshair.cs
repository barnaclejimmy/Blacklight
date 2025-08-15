using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Sprite crosshair;
    public Sprite interact;

    private void Update()
    {
        Vector2 mouseWorldPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseWorldPosition;
    }

    public void Grab()
    {
        GetComponent<SpriteRenderer>().sprite = interact;
        transform.localScale = new Vector2(0.05f, 0.05f);
    }

    public void UnGrab()
    {
        GetComponent<SpriteRenderer>().sprite = crosshair;
        transform.localScale = new Vector2(0.1f, 0.1f);
    }
}
