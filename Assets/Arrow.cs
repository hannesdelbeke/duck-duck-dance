using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Direction direction;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float speed = 2f;
    public float inputOffset = 1f; // Offset from the input area

    public Color failColor = Color.red; // Color to change when arrow fails

    private SpriteRenderer spriteRenderer;
    private bool failed = false;

    private InputArea inputArea;

    private void Awake()
    {
        inputArea = InputArea.Instance;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetArrowSprite();
    }

    void Update()
    {

        if (!failed)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            // Check if the arrow goes below the input area and input offset
            if (transform.position.y < (inputArea.transform.position.y - inputOffset))
            {
                ChangeColorOnFail();
            }

            // Destroy the arrow if it goes below the screen
            if (transform.position.y < -10f)
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            // Destroy the arrow after a delay
            transform.Translate(Vector2.down * speed * Time.deltaTime / 2);
        }
    }

    void SetArrowSprite()
    {
        switch (direction)
        {
            case Direction.Up:
                spriteRenderer.sprite = upSprite;
                break;
            case Direction.Down:
                spriteRenderer.sprite = downSprite;
                break;
            case Direction.Left:
                spriteRenderer.sprite = leftSprite;
                break;
            case Direction.Right:
                spriteRenderer.sprite = rightSprite;
                break;
        }
    }

    void ChangeColorOnFail()
    {
        failed = true;
        spriteRenderer.color = failColor;
    }

    public bool IsWithinInputArea(Vector3 inputAreaPosition, float inputThreshold)
    {
        return Vector3.Distance(transform.position, inputAreaPosition) <= inputThreshold;
    }
}