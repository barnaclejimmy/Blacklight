using UnityEngine;

public class DoorKey : MonoBehaviour
{
    private Door doorScript;
    private PlayerAudio playerAudio;
    private bool keyFound;
    private SpriteRenderer keyRenderer;
    private Color keyColor;
    [SerializeField] private Texture keyTexture;
    [SerializeField] private bool finalDoor = false;

    private void Start()
    {
        doorScript = transform.parent.GetChild(1).GetComponent<Door>();
        doorScript.keyRequired = true;
        keyFound = false;
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAudio>();
        keyRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !keyFound)
        {
            playerAudio.KeyPickup();
            doorScript.keyObtained = true;
            keyRenderer.sortingOrder = 20;
            keyColor = keyRenderer.color;
            keyRenderer.enabled = false;
            keyFound = true;
            if (finalDoor)
            {
                EndTrigger.key = true;
            }
        }
    }

    private void OnGUI()
    {
        if (keyFound)
        {
            GUI.color = keyColor;
            GUI.DrawTexture(new Rect(20, Screen.height - 95, 70, 70), keyTexture, ScaleMode.ScaleToFit);
        }
    }
}
