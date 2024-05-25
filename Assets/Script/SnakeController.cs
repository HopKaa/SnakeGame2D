using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform gameField;
    [SerializeField] private float moveInterval = 0.1f;
    [SerializeField] private float speedMultiplier = 10.0f;
    [SerializeField] private AppleManager appleManager;
    [SerializeField] private float segmentDistance = 8f;
    [SerializeField] private GameObject gameOverText;

    private float nextMoveTime;
    private List<GameObject> snakeParts = new List<GameObject>();
    private Vector2 direction = Vector2.right;
    private bool growSnakeNextMove = false;
    private List<Vector2> previousPositions = new List<Vector2>();

    void Start()
    {
        RectTransform gameFieldRectTransform = gameField.GetComponent<RectTransform>();
        float gameFieldWidth = gameFieldRectTransform.rect.width;
        float gameFieldHeight = gameFieldRectTransform.rect.height;

        Vector3 spawnPosition = new Vector3(gameFieldWidth / 2, gameFieldHeight / 2, 0f);

        GameObject head = Instantiate(headPrefab, spawnPosition, Quaternion.identity, gameField);
        snakeParts.Add(head);

        nextMoveTime = Time.time + (moveInterval / speedMultiplier);
    }

    void Update()
    {
        Vector2 previousDirection = direction;

        if (Input.GetKeyDown(KeyCode.W) && previousDirection != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && previousDirection != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && previousDirection != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && previousDirection != Vector2.left)
        {
            direction = Vector2.right;
        }

        if (Time.time >= nextMoveTime)
        {
            Move();
            nextMoveTime = Time.time + (moveInterval / speedMultiplier);
        }
    }

    void Move()
    {
        Vector2 newPosition = (Vector2)snakeParts[0].transform.localPosition + direction * segmentDistance; // Используем переменную segmentDistance

        previousPositions.Insert(0, snakeParts[0].transform.localPosition);
        snakeParts[0].transform.localPosition = newPosition;

        for (int i = 1; i < snakeParts.Count; i++)
        {
            if (previousPositions.Count >= i * segmentDistance)
            {
                snakeParts[i].transform.localPosition = previousPositions[Mathf.RoundToInt(i * segmentDistance)];
            }
        }

        // Ограничиваем количество сохраненных позиций головы
        if (previousPositions.Count > snakeParts.Count * segmentDistance)
        {
            previousPositions.RemoveRange(Mathf.RoundToInt(snakeParts.Count * segmentDistance), previousPositions.Count - Mathf.RoundToInt(snakeParts.Count * segmentDistance));
        }

        if (growSnakeNextMove)
        {
            growSnakeNextMove = false;
            Vector2 newPartPosition = snakeParts[snakeParts.Count - 1].transform.localPosition;
            GameObject newBodyPart = Instantiate(bodyPrefab, newPartPosition, Quaternion.identity, gameField);
            snakeParts.Add(newBodyPart);
        }

        CheckCollisionWithApple(snakeParts[0].GetComponent<Collider2D>());
        CheckCollisionWithBarrier(snakeParts[0].GetComponent<Collider2D>());
    }

    void CheckCollisionWithApple(Collider2D headCollider)
    {
        if (headCollider != null && appleManager != null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(headCollider.bounds.center, headCollider.bounds.size, 0f);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.GetComponent<Apple>() != null)
                {
                    HandleAppleCollision(collider.gameObject);
                }
            }
        }
    }

    void CheckCollisionWithBarrier(Collider2D headCollider)
    {
        if (headCollider != null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(headCollider.bounds.center, headCollider.bounds.size, 0f);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.name.Contains("Barrier"))
                {
                    HandleGameOver();
                    break;
                }
            }
        }
    }

    void HandleAppleCollision(GameObject apple)
    {
        Destroy(apple);
        appleManager.SpawnApple();
        GrowSnake();
    }

    void HandleGameOver()
    {
        if (gameOverText != null)
        {
            Text gameOverTextField = gameOverText.GetComponentInChildren<Text>();
            if (gameOverTextField != null)
            {
                gameOverTextField.text = "GAME OVER";
            }

            gameOverText.SetActive(true);
        }

        Time.timeScale = 0;
    }

    void GrowSnake()
    {
        growSnakeNextMove = true;
    }
}
