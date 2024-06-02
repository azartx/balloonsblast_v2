using System.Collections.Generic;
using UnityEngine;

public class AudioLoader {
    
    private static List<System.Lazy<AudioClip>> balloonClaps = new List<System.Lazy<AudioClip>>
    {
        new System.Lazy<AudioClip>(() => ClapByPath("sounds/claps/clap_1")),
        new System.Lazy<AudioClip>(() => ClapByPath("sounds/claps/clap_2"))
    };
    
    public static AudioClip RandomClap()
    {
        System.Random random = new System.Random();
        return balloonClaps[random.Next(balloonClaps.Count)].Value;
    }
    
    private static AudioClip ClapByPath(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}