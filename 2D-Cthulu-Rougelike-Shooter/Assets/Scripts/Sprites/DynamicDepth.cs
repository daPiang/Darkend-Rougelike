using UnityEngine;

public class DynamicDepth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int depthPrecision;
    // private Camera mainCamera;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // mainCamera = Camera.main;
    }

    private void Update()
    {
        if (spriteRenderer)
        {
            spriteRenderer.sortingOrder = (int)(-transform.position.y * depthPrecision);
        }
    }
}
