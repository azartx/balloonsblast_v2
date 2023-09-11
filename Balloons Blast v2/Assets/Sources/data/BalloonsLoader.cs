using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Sources.data
{
    public class BalloonsLoader
    {
        public static BalloonsLoader instance = new();
        
        private BalloonsLoader() {}
        
        public Dictionary<string, Sprite> spriteDic = new();
        
        public Task<Dictionary<string, Sprite>> loadBalloons()
        {
            var balloonSprites = Resources.LoadAll<Sprite>("balloons");
            
            if (balloonSprites == null || balloonSprites.Length <= 0)
            {
                Debug.LogError("The Provided Base-Atlas Sprite `" + "balloons" + "` does not exist!");
                return Task.FromResult<Dictionary<string, Sprite>>(null);
            }

            foreach (var t in balloonSprites)
            {
                Debug.Log(t.name + " ---- ");
                spriteDic.Add(t.name, t);
            }

            return Task.FromResult(spriteDic);
        }
    }
}
