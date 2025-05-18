using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Game;

public static class GameScoreManager
{
    private static readonly int DEF_SCORE_UP = 100;

    private static int score = 0;

    public static void UpdateScore(double speed)
    {
        score = score + DEF_SCORE_UP + (int)(DEF_SCORE_UP * (speed + 1));
    }

    public static int GetScore()
    {
        return score;
    }

    public static void Clear()
    {
        score = 0;
    }
}