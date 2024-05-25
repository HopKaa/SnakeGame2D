using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    [SerializeField] private Transform gameField;
    [SerializeField] private GameObject barrierPrefab;
    [SerializeField] private float barrierThickness = 40f;

    void Start()
    {
        RectTransform gameFieldRectTransform = gameField.GetComponent<RectTransform>();
        float gameFieldWidth = gameFieldRectTransform.rect.width;
        float gameFieldHeight = gameFieldRectTransform.rect.height;

        // Создаем барьеры
        CreateBarrier(new Vector2(0, gameFieldHeight / 2 - barrierThickness / 2), new Vector2(gameFieldWidth, barrierThickness)); // Верхний барьер
        CreateBarrier(new Vector2(0, -gameFieldHeight / 2 + barrierThickness / 2), new Vector2(gameFieldWidth, barrierThickness)); // Нижний барьер
        CreateBarrier(new Vector2(-gameFieldWidth / 2 + barrierThickness / 2, 0), new Vector2(barrierThickness, gameFieldHeight)); // Левый барьер
        CreateBarrier(new Vector2(gameFieldWidth / 2 - barrierThickness / 2, 0), new Vector2(barrierThickness, gameFieldHeight)); // Правый барьер
    }

    void CreateBarrier(Vector2 position, Vector2 size)
    {
        GameObject barrier = Instantiate(barrierPrefab, gameField);
        RectTransform barrierRectTransform = barrier.GetComponent<RectTransform>();
        barrierRectTransform.sizeDelta = size;
        barrierRectTransform.anchoredPosition = position;

        // Настроим коллайдер барьера
        BoxCollider2D barrierCollider = barrier.GetComponent<BoxCollider2D>();
        if (barrierCollider != null)
        {
            barrierCollider.size = size;
        }
    }
}