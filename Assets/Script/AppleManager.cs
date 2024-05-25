using UnityEngine;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private Transform gameField;

    private float appleLifetime = 15f;
    private GameObject currentApple;
    private float gameFieldWidth;
    private float gameFieldHeight;
    private float barrierThickness = 40f; // Толщина барьеров
    private float timeSinceLastAppleSpawn;

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

        Vector2 randomPosition = GetRandomPosition();

        currentApple = Instantiate(applePrefab, gameField);
        RectTransform appleRectTransform = currentApple.GetComponent<RectTransform>();
        appleRectTransform.anchoredPosition = randomPosition;

        BoxCollider2D appleCollider = currentApple.GetComponent<BoxCollider2D>();
        if (appleCollider != null)
        {
            appleCollider.size = new Vector2(50, 50);
        }

        timeSinceLastAppleSpawn = Time.time;
    }

    private Vector2 GetRandomPosition()
    {
        float minX = -gameFieldWidth / 2 + barrierThickness;
        float maxX = gameFieldWidth / 2 - barrierThickness;
        float minY = -gameFieldHeight / 2 + barrierThickness;
        float maxY = gameFieldHeight / 2 - barrierThickness;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }
}