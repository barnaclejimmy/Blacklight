using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    void Start() {
        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    void LateUpdate() {
        bool shouldMove = Player.cursorMove;
        if (shouldMove) {
            // Get the world position of the mouse
            Vector2 mouseWorldPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseWorldPosition;
        }
    }
}