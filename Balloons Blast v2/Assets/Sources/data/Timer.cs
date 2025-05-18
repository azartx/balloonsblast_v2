using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Game;

public class Timer : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float startTime = 60f; // Начальное время в секундах
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    public GameView gameView;
    public GameObject endGamePannel;

    private float currentTime;
    private Coroutine timerCoroutine;

    public void StartGameTimer()
    {
        timerPanel.SetActive(true);
        // Инициализация таймера
        currentTime = startTime;
        UpdateTimerDisplay();

        // Запуск корутины
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // Ждем ровно 1 секунду
            currentTime -= 1f;
            UpdateTimerDisplay();
        }

        TimerCompleted();
    }

    void UpdateTimerDisplay()
    {
        // Форматирование времени в MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerCompleted()
    {
        gameView.StopGame();
        endGamePannel.SetActive(true);
        // Действия по завершению таймера
        timerText.text = "00:00";
        Debug.Log("Время вышло!");
    }

    // Для остановки таймера при необходимости
    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }
}