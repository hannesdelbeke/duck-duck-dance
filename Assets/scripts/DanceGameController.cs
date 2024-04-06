using UnityEngine;

public class DanceGameController : MonoBehaviour
{
    public AudioClip quackSound;

    public Color successFlashColor = Color.white; // Color for success flash
    public Color failFlashColor = Color.red; // Color for fail flash
    public GameObject upFlashObject;
    public GameObject downFlashObject;
    public GameObject leftFlashObject;
    public GameObject rightFlashObject;

    public Sprite neutralSprite1;
    public Sprite neutralSprite2;
    public Sprite leftSprite;
    public Sprite leftFailSprite;
    public Sprite rightSprite;
    public Sprite rightFailSprite;
    public Sprite upSprite;
    public Sprite upFailSprite;
    public Sprite downSprite;
    public Sprite downFailSprite;

    public float poseTime = 1.0f; // Duration to hold a pose in seconds
    public float neutralSwapInterval = 0.5f; // Interval to swap between neutral sprites

    private SpriteRenderer spriteRenderer;
    private float poseTimer = 0.0f;
    private float neutralSwapTimer = 0.0f;
    private bool holdingPose = false;
    private bool usingNeutralSprite1 = true;

    private InputArea inputArea;

    void Start()
    {
        inputArea = FindObjectOfType<InputArea>(); // Find the InputArea script in the scene

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found.");
        }
    }

    void SwapNeutralSprite()
    {
        usingNeutralSprite1 = !usingNeutralSprite1;
        SetSprite(GetNeutralSprite());
    }

    Sprite GetNeutralSprite()
    {
        return usingNeutralSprite1 ? neutralSprite1 : neutralSprite2;
    }

    void playQuackSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(0.9f, 1.1f);
        audio.PlayOneShot(quackSound);
    }
    Sprite GetDirectionSprite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return upSprite;
            case Direction.Down:
                return downSprite;
            case Direction.Left:
                return leftSprite;
            case Direction.Right:
                return rightSprite;
            default:
                return neutralSprite1; // Return a default sprite if direction is not recognized
        }
    }

    Sprite GetFailSprite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return upFailSprite;
            case Direction.Down:
                return downFailSprite;
            case Direction.Left:
                return leftFailSprite;
            case Direction.Right:
                return rightFailSprite;
            default:
                return neutralSprite1; // Return a default sprite if direction is not recognized
        }
    }

    void Update()
    {
        // If not holding a pose, swap between neutral sprites
        if (!holdingPose)
        {
            // If no arrow keys are pressed, swap between neutral sprites
            if (Time.time >= neutralSwapTimer)
            {
                SwapNeutralSprite();
                neutralSwapTimer = Time.time + neutralSwapInterval;
            }
        }

        // If holding a pose, decrement the pose timer
        if (holdingPose)
        {
            poseTimer -= Time.deltaTime;
            if (poseTimer <= 0)
            {
                holdingPose = false;
                SetSprite(GetNeutralSprite());
            }
        }
    }

    public void ProcessInput(Direction direction)
    {
        // if direction is same as current direction, dont change sprite
        if (holdingPose && (spriteRenderer.sprite == GetDirectionSprite(direction) || spriteRenderer.sprite == GetFailSprite(direction)))
        {
            return;
        }

        if (CheckInput(direction))
        {
            SetSpriteAndStartTimer(GetDirectionSprite(direction));
        }
        else
        {
            SetSpriteAndStartTimer(GetFailSprite(direction));
        }
    }

    bool CheckInput(Direction expectedDirection)
    {
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject arrow in arrows)
        {
            Arrow arrowComponent = arrow.GetComponent<Arrow>();
            if (Mathf.Abs(arrow.transform.position.y - inputArea.gameObject.transform.position.y) <= inputArea.inputThreshold && arrowComponent.direction == expectedDirection)
            {
                Destroy(arrow);
                Debug.Log("Success - Player pressed the correct input for the arrow.");

                // Player pressed the correct input for the arrow
                // Add scoring or other gameplay logic here
                FlashObject(expectedDirection, successFlashColor); // Flash on succes input
                return true;
            }
        }

        // Player missed the arrow or pressed the wrong input
        Debug.Log("Fail - Player missed the arrow or pressed the wrong input.");
        FlashObject(expectedDirection, failFlashColor); // Flash on failed input
        // Add logic for missed arrow or wrong input here
        return false;
    }

    void FlashObject(Direction direction, Color flashColor)
    {
        GameObject objectToFlash = null;
        switch (direction)
        {
            case Direction.Up:
                objectToFlash = upFlashObject;
                break;
            case Direction.Down:
                objectToFlash = downFlashObject;
                break;
            case Direction.Left:
                objectToFlash = leftFlashObject;
                break;
            case Direction.Right:
                objectToFlash = rightFlashObject;
                break;
        }

        if (objectToFlash != null)
        {
            ArrowFlash arrowFlash = objectToFlash.GetComponent<ArrowFlash>();
            if (arrowFlash != null)
            {
                arrowFlash.Flash(flashColor);
            }
            else
            {
                Debug.LogWarning("ArrowFlash component not found on the object to flash.");
            }
        }
        else
        {
            Debug.LogWarning("No object assigned to flash for direction: " + direction);
        }
    }

    void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    void SetSpriteAndStartTimer(Sprite sprite)
    {
        SetSprite(sprite);
        holdingPose = true;
        poseTimer = poseTime;
        playQuackSound();
    }
}

