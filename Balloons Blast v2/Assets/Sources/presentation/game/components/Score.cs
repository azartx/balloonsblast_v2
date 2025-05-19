using TMPro;
using UnityEngine;

class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = GameScoreManager.GetScore().ToString();
    }
}