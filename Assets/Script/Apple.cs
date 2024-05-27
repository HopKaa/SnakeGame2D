using System.Collections;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    private float blinkDuration;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartBlinking(float duration)
    {
        
    }

    private IEnumerator Blink()
    {
        float endTime = Time.time + blinkDuration;
        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.5f);
        }
        spriteRenderer.enabled = true;
        isBlinking = false;
    }
}