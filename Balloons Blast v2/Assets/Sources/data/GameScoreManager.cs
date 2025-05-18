using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Game;

//TODO: складывать счет в рамках игры в список (на случай если будет несколько игр за одну сессию (когда игрок в конце раунда нажимает на играть сначала
// и по выходу забирать из списка лучший счет. список после этого чистить
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