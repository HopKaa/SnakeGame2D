using UnityEngine;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private Transform gameField;
    private float appleLifetime = 15f; // ¬рем€ жизни €блока
    private float timeSinceLastAppleSpawn;

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

    void Update()
    {
        if (Time.time - timeSinceLastAppleSpawn >= appleLifetime)
        {
            SpawnApple();
        }
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
            appleCollider.size = new Vector2(50, 50); // ”величиваем размер коллайдера дл€ €блока
        }

        // ”ничтожаем €блоко после заданного времени
        Destroy(currentApple, appleLifetime);
        timeSinceLastAppleSpawn = Time.time;
    }
}