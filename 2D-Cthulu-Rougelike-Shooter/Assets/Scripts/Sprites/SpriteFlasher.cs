using System.Collections;
using Fusion;
using UnityEngine;

public class SpriteFlasher : NetworkBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.5f;
    public Color flashColor = Color.red;

    private Color originalColor;

    private void Start()
    {
        // Store the original color of the sprite
        originalColor = spriteRenderer.color;
    }

    public void FlashRed()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            // Alternate between the flash color and original color
            spriteRenderer.color = (spriteRenderer.color == flashColor) ? originalColor : flashColor;

            // Wait for a short time
            yield return new WaitForSeconds(0.1f);

            elapsedTime += 0.1f;
        }

        // Ensure the sprite returns to its original color
        spriteRenderer.color = originalColor;
    }
}
