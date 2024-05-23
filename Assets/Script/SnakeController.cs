using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform gameField;
    [SerializeField] private float moveInterval = 0.5f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private AppleManager appleManager;

    private float nextMoveTime;
    private List<GameObject> snakeParts = new List<GameObject>();
    private Vector2 direction = Vector2.right;
    private bool growSnakeNextMove = false;

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
        Vector2 newPosition = (Vector2)snakeParts[0].transform.localPosition + direction;

        snakeParts[0].transform.localPosition = newPosition;

        for (int i = snakeParts.Count - 1; i > 0; i--)
        {
            snakeParts[i].transform.localPosition = snakeParts[i - 1].transform.localPosition;
        }

        if (growSnakeNextMove)
        {
            growSnakeNextMove = false;
            GameObject newBodyPart = Instantiate(bodyPrefab, snakeParts[snakeParts.Count - 1].transform.localPosition, Quaternion.identity, snakeParts[snakeParts.Count - 1].transform);
            snakeParts.Add(newBodyPart);
        }

        CheckCollisionWithApple(snakeParts[0].GetComponent<Collider2D>());
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

    void HandleAppleCollision(GameObject apple)
    {
        Destroy(apple);
        appleManager.SpawnApple();
        GrowSnake();
    }

    void GrowSnake()
    {
        growSnakeNextMove = true;
    }
}