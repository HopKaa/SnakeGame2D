using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    private CanvasRenderer canvasRenderer;
    private Image image;
    private float blinkTime;
    private float blinkInterval = 0.1f;
    private bool isBlinking = false;
    private float blinkTimer = 0f;

    void Start()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        image = GetComponent<Image>();
        if (canvasRenderer == null)
        {
            Debug.LogError("Missing CanvasRenderer component on Apple");
        }
        if (image == null)
        {
            Debug.LogError("Missing Image component on Apple");
        }
    }

    void Update()
    {
        if (isBlinking)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() == 1f ? 0f : 1f);
                blinkTimer = 0f;
            }
        }
    }

    public void StartBlinking(float time)
    {
        blinkTime = time;
        isBlinking = true;
        Invoke("DestroyApple", blinkTime);
    }

    private void DestroyApple()
    {
        Destroy(gameObject);
    }
}