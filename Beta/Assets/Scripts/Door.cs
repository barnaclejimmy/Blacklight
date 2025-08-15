using UnityEngine;

public class Door : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioSource audioSource;
    [SerializeField] private AudioClip openSound, closeSound, lockedSound;
    [HideInInspector] public bool keyRequired, keyObtained;
    private SpriteRenderer doorRenderer;
    [SerializeField] private Sprite unlockedDoor;
    private PlayerAudio playerAudio;
    [HideInInspector] public bool oneWay;
    private bool unlocked;
    private string message;
    private float messageTimer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();
        keyRequired = false;
        keyObtained = false;
        doorRenderer = GetComponent<SpriteRenderer>();
        unlocked = false;
        message = null;
        messageTimer = 0;
    }

    private void Start()
    {
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAudio>();
    }

    public void Open()
    {
        if (oneWay)
        {
            audioSource.PlayOneShot(lockedSound);
            message = "There's no going back.";
            messageTimer = 5;
        }
        else if (body.isKinematic && (keyRequired == keyObtained))
        {
            if (keyObtained && !unlocked)
            {
                doorRenderer.sprite = unlockedDoor;
                playerAudio.KeyUnlock();
                transform.parent.GetChild(3).gameObject.SetActive(false);
                unlocked = true;
            }
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
            audioSource.PlayOneShot(lockedSound);
            message = "It seems to be locked.";
            messageTimer = 5;
        }
        else
        {
            Shut();
        }
    }

    public void Shut()
    {
        if (body.isKinematic)
        {
            audioSource.PlayOneShot(lockedSound);
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

    private void OnGUI()
    {
        if (messageTimer >= 0)
        {
            GUIStyle style = new GUIStyle
            {
                fontSize = 40,
                alignment = TextAnchor.UpperCenter
            };
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(Screen.width/2-50, Screen.height/2+200, 100, 100), message, style);
            messageTimer -= Time.deltaTime;
        }
    }
}
