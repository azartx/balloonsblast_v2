using System.Collections;
using System.Collections.Generic;

public static class StaticPreferences
{
    public static int GameType = GAME_TYPE_FREE;

    public static readonly int GAME_TYPE_FREE = 0;
    public static readonly int GAME_TYPE_TIMED = 1;

    public static bool IsTimedGame()
    {
        return GameType == GAME_TYPE_TIMED;
    }
}