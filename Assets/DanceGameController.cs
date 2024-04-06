using UnityEngine;

public class DanceGameController : MonoBehaviour
{
    public Color successFlashColor = Color.white; // Color for success flash
    public Color failFlashColor = Color.red; // Color for fail flash
    public GameObject upFlashObject;
    public GameObject downFlashObject;
    public GameObject leftFlashObject;
    public GameObject rightFlashObject;

    public Sprite neutralSprite;
    public Sprite leftSprite;
    public Sprite leftFailSprite;
    public Sprite rightSprite;
    public Sprite rightFailSprite;
    public Sprite upSprite;
    public Sprite upFailSprite;
    public Sprite downSprite;
    public Sprite downFailSprite;

    public float poseTime = 1.0f; // Duration to hold a pose in seconds

    private SpriteRenderer spriteRenderer;
    private float poseTimer = 0.0f;
    private bool holdingPose = false;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CheckInput(Direction.Left))
                SetSpriteAndStartTimer(leftSprite);
            else
                SetSpriteAndStartTimer(leftFailSprite);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CheckInput(Direction.Right))
                SetSpriteAndStartTimer(rightSprite);
            else
                SetSpriteAndStartTimer(rightFailSprite);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CheckInput(Direction.Up))
                SetSpriteAndStartTimer(upSprite);
            else
                SetSpriteAndStartTimer(upFailSprite);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CheckInput(Direction.Down))
                SetSpriteAndStartTimer(downSprite);
            else
                SetSpriteAndStartTimer(downFailSprite);
        }
        else if (!holdingPose)
        {
            // If none of the arrow keys are pressed and not holding a pose, show neutral pose
            SetSprite(neutralSprite);
        }

        if (holdingPose)
        {
            poseTimer -= Time.deltaTime;
            if (poseTimer <= 0)
            {
                holdingPose = false;
                SetSprite(neutralSprite); // Return to neutral sprite after holding pose
            }
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
    }
}
