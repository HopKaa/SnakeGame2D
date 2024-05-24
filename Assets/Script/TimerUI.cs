using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerText;
    private float timeElapsed = 0f;

    void Update()
    {
        // Увеличиваем время, прошедшее с начала игры
        timeElapsed += Time.deltaTime;

        // Форматируем время в минуты и секунды
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Обновляем текст таймера
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}