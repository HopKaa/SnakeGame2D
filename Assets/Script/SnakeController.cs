using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform gameField;
    [SerializeField] private float moveInterval = 0.5f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private AppleManager appleManager; // ������ �� ��������� AppleManager
    private float nextMoveTime;
    private List<GameObject> snakeParts = new List<GameObject>();
    private Vector2 direction = Vector2.right;
    private Vector2 previousDirection = Vector2.right;

    void Start()
    {
        // �������� ������� �������� ����
        RectTransform gameFieldRectTransform = gameField.GetComponent<RectTransform>();
        float gameFieldWidth = gameFieldRectTransform.rect.width;
        float gameFieldHeight = gameFieldRectTransform.rect.height;

        // ������������ ���������� ������ ������ ���� � ������ �������� ����
        Vector3 spawnPosition = new Vector3(gameFieldWidth / 2, gameFieldHeight / 2, 0f);

        // ������� ������ ���� � ������ �������� ����
        GameObject head = Instantiate(headPrefab, spawnPosition, Quaternion.identity, gameField);
        snakeParts.Add(head);

        // ������������� ����� ���������� �����������
        nextMoveTime = Time.time + (moveInterval / speedMultiplier); // ��������� ��������� ��������
    }

    void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            Move();
            nextMoveTime = Time.time + (moveInterval / speedMultiplier);
        }

        // ��������� ����� ������ ��� ��������� ����������� ����
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
    }
    void Move()
    {
        // �������� ����� ������� ������ ���� �� ������ �������� �����������
        Vector2 newPosition = (Vector2)snakeParts[0].transform.position + direction;

        // ������� ����� ������ ����
        GameObject newHead = Instantiate(headPrefab, newPosition, Quaternion.identity, gameField);
        snakeParts.Insert(0, newHead);

        // ���������� ��������� ����� ���� ����, ����� ��� ���������
        if (snakeParts.Count > 1)
        {
            GameObject tail = snakeParts[snakeParts.Count - 1];
            snakeParts.RemoveAt(snakeParts.Count - 1);
            Destroy(tail);
        }
        previousDirection = direction;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Apple>() != null)
        {
            HandleAppleCollision(other.gameObject);
        }
    }
    void HandleAppleCollision(GameObject apple)
    {
        // ���������� ������
        Destroy(apple);

        // ����������� ������
        GrowSnake();
    }
    void GrowSnake()
    {
        // ������� ����� ����� ���� ����
        GameObject newBodyPart = Instantiate(bodyPrefab, snakeParts[snakeParts.Count - 1].transform.position, Quaternion.identity, gameField);
        snakeParts.Add(newBodyPart);
    }
}