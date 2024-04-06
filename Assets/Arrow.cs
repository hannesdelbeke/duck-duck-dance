using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Direction direction;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float speed = 2f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetArrowSprite();
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Destroy the arrow if it goes below the screen
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
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

    public bool IsWithinInputArea(Vector3 inputAreaPosition, float inputThreshold)
    {
        return Vector3.Distance(transform.position, inputAreaPosition) <= inputThreshold;
    }
}
