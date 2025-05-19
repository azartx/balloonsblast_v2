using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Game;

public class Timer : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private float startTime = 60f; // ��������� ����� � ��������
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    public GameView gameView;
    public GameObject endGamePannel;

    private float currentTime;
    private Coroutine timerCoroutine;

    public void StartGameTimer()
    {
        timerPanel.SetActive(true);
        // ������������� �������
        currentTime = startTime;
        UpdateTimerDisplay();

        // ������ ��������
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // ���� ����� 1 �������
            currentTime -= 1f;
            UpdateTimerDisplay();
        }

        TimerCompleted();
    }

    void UpdateTimerDisplay()
    {
        // �������������� ������� � MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerCompleted()
    {
        gameView.StopGame();
        endGamePannel.SetActive(true);
        // �������� �� ���������� �������
        timerText.text = "00:00";
        Debug.Log("����� �����!");
    }

    // ��� ��������� ������� ��� �������������
    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }
}