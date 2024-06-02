using System.Collections.Generic;
using UnityEngine;

public class ExplosionLoader {
    
    private static List<System.Lazy<GameObject>> prefabsPaths = new List<System.Lazy<GameObject>>
    {
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_1")),
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_2")),
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_3")),
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_4")),
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_5")),
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_6")),
        new System.Lazy<GameObject>(() => ExplosionByPath("balloon_pop_prefabs/pop_7"))
    };
    
    public static GameObject GetExplosion(GameObject balloon)
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(prefabsPaths.Count);
    
        return prefabsPaths[randomIndex].Value;
    }
    
    private static GameObject ExplosionByPath(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}