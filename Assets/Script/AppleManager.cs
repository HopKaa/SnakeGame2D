using UnityEngine;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private Transform gameField;
    [SerializeField] private float appleBlinkTime = 5.0f;

    private GameObject currentApple;
    private float gameFieldWidth;
    private float gameFieldHeight;

    void Start()
    {
        RectTransform gameFieldRectTransform = gameField.GetComponent<RectTransform>();
        gameFieldWidth = gameFieldRectTransform.rect.width;
        gameFieldHeight = gameFieldRectTransform.rect.height;

        SpawnApple();
    }

    public void SpawnApple()
    {
        if (currentApple != null)
        {
            Destroy(currentApple);
        }

        Vector2 randomPosition = new Vector2(Random.Range(-gameFieldWidth / 2, gameFieldWidth / 2),
                                             Random.Range(-gameFieldHeight / 2, gameFieldHeight / 2));

        currentApple = Instantiate(applePrefab, gameField);
        RectTransform appleRectTransform = currentApple.GetComponent<RectTransform>();
        appleRectTransform.anchoredPosition = randomPosition;

        BoxCollider2D appleCollider = currentApple.GetComponent<BoxCollider2D>();
        if (appleCollider != null)
        {
            appleCollider.size = new Vector2(20, 20); // ”величиваем размер коллайдера дл€ €блока
        }

        Apple appleComponent = currentApple.GetComponent<Apple>();
        if (appleComponent != null)
        {
            appleComponent.StartBlinking(appleBlinkTime);
        }

        // Automatically respawn a new apple after a certain time
        Invoke("SpawnApple", appleBlinkTime);
    }
}