using UnityEngine;
using UnityEngine.UI;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private Transform gameField;
    private GameObject currentApple;
    [SerializeField] private float appleLifetime = 10.0f;
    [SerializeField] private float appleSpawnInterval = 2.0f;
    [SerializeField] private float appleBlinkTime = 2.0f;

    private float gameFieldWidth;
    private float gameFieldHeight;

    void Start()
    {
        // Получаем размеры gameField
        RectTransform gameFieldRectTransform = gameField.GetComponent<RectTransform>();
        gameFieldWidth = gameFieldRectTransform.rect.width;
        gameFieldHeight = gameFieldRectTransform.rect.height;

        InvokeRepeating("SpawnApple", appleSpawnInterval, appleLifetime);
    }

    void SpawnApple()
    {
        if (currentApple != null)
        {
            Destroy(currentApple);
        }

        // Генерируем случайные координаты в пределах gameField
        Vector2 randomPosition = new Vector2(Random.Range(-gameFieldWidth / 2, gameFieldWidth / 2),
                                             Random.Range(-gameFieldHeight / 2, gameFieldHeight / 2));

        currentApple = Instantiate(applePrefab, gameField);
        currentApple.GetComponent<RectTransform>().anchoredPosition = randomPosition;
        currentApple.GetComponent<Apple>().StartBlinking(appleBlinkTime);
    }
}
