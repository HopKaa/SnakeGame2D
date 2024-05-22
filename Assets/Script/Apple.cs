using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Apple : MonoBehaviour
{
    private float blinkTime;
    private bool isBlinking = false;

    public void StartBlinking(float time)
    {
        blinkTime = time;
        isBlinking = true;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        Image appleImage = GetComponent<Image>();
        while (isBlinking)
        {
            appleImage.enabled = !appleImage.enabled;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void OnDestroy()
    {
        isBlinking = false;
    }
}