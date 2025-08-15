using UnityEngine;

public class Door : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioSource audioSource;
    [SerializeField] private AudioClip openSound, closeSound;
    [HideInInspector] public bool keyRequired;
    [HideInInspector] public bool keyObtained;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();
        keyRequired = false;
        keyObtained = false;
    }

    public void Open()
    {
        if (body.isKinematic && (keyRequired == keyObtained))
        {
            audioSource.PlayOneShot(openSound);
            body.isKinematic = false;
            if (DoorOpen.inLeft)
            {
                body.AddForce((Vector2.right + Vector2.up) * 15, ForceMode2D.Impulse);
            }
            else
            {
                body.AddForce((Vector2.left + Vector2.down) * 15, ForceMode2D.Impulse);
            }
            transform.parent.GetChild(0).tag = "Untagged";
        }
        else if (body.isKinematic && (keyRequired != keyObtained))
        {
            Debug.Log("you need the key");
        }
        else
        {
            audioSource.PlayOneShot(closeSound);
            body.isKinematic = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = 0;
            transform.position = startPosition;
            transform.rotation = startRotation;
            transform.parent.GetChild(0).tag = "Wall";
        }
    }
}
