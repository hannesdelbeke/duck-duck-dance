using UnityEngine;

public class DanceGameController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite neutralSprite;
    public Sprite failSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite upSprite;
    public Sprite downSprite;

    public float poseDuration = 1.0f; // Duration to hold a pose in seconds

    private float poseTimer = 0.0f;
    private bool holdingPose = false;

    void Start()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetSprite(leftSprite);
            StartPoseTimer();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetSprite(rightSprite);
            StartPoseTimer();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetSprite(upSprite);
            StartPoseTimer();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetSprite(downSprite);
            StartPoseTimer();
        }
        else if (!holdingPose)
        {
            // If none of the arrow keys are pressed and not holding a pose, show neutral sprite
            SetSprite(neutralSprite);
        }

        if (holdingPose)
        {
            poseTimer -= Time.deltaTime;
            if (poseTimer <= 0)
            {
                holdingPose = false;
            }
        }
    }

    void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    void StartPoseTimer()
    {
        holdingPose = true;
        poseTimer = poseDuration;
    }
}
