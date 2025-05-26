using UnityEngine;
using TMPro; // For TextMeshPro

public class LeaderboardEntryUI : MonoBehaviour
{
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI scoreText;

    public void SetEntry(string userName, int score)
    {
        if (userNameText != null)
            userNameText.text = userName;
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
}
