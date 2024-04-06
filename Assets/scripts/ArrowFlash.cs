using UnityEngine;
using System.Collections;

public class ArrowFlash : MonoBehaviour
{
    public float flashDuration = 0.2f; // Duration of the flash effect

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found.");
            enabled = false; // Disable the script if SpriteRenderer is not found
            return;
        }

        originalColor = spriteRenderer.color;
    }

    public void Flash(Color flashColor)
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashCoroutine(flashColor));
    }

    IEnumerator FlashCoroutine(Color flashColor)
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
