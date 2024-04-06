using UnityEngine;
using System.Collections;


public class SpriteSwapper : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float swapInterval = 1.0f; // Interval between sprite swaps

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found.");
        }

        // Start the coroutine to swap sprites
        StartCoroutine(SwapSprites());
    }

    IEnumerator SwapSprites()
    {
        while (true)
        {
            // Swap sprites
            if (spriteRenderer.sprite == sprite1)
            {
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }

            // Wait for the specified interval before swapping again
            yield return new WaitForSeconds(swapInterval);
        }
    }
}
