using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using System;

namespace Sources.data
{
    public class BalloonsLoader
    {
        private BalloonsLoader() {}
        
        private static readonly string[] spriteNames =
        {
            "black",
                "blue",
                "green",
                "grey",
                "orange",
                "pink",
                "red",
                "white"
        };

        private static Lazy<SpriteAtlas> lazyAtlas =
            new Lazy<SpriteAtlas>(() => loadBalloons());
        
        private static SpriteAtlas loadBalloons()
        {
            SpriteAtlas atlas = Resources.Load<SpriteAtlas>("balloons_atlas");

            if (atlas == null)
            {
                Debug.LogError("Атлас не найден: balloons_atlas");
            }

            return atlas;
        }

        public static Sprite getRandomBalloonSprite()
        {
            return lazyAtlas.Value
                .GetSprite(spriteNames[UnityEngine.Random.Range(0, spriteNames.Length)]);
        }
    }
}
