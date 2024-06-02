using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationAudioController
{
    
    private static bool _isAudioEnabled = true;
    
    public static void ChangeAudioState(bool isAudioEnabled)
    {
        _isAudioEnabled = isAudioEnabled;
    }
    
    public static bool IsAudioEnabled()
    {
        return _isAudioEnabled;
    }
}