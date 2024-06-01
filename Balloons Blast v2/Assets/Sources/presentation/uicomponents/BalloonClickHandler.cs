using UnityEngine;
using System.Collections.Generic;

public class BalloonClickHandler : MonoBehaviour {
    
    private static List<string> prefabsPaths = new List<string>
    {
        "balloon_pop_prefabs/pop_1",
        "balloon_pop_prefabs/pop_2",
        "balloon_pop_prefabs/pop_3",
        "balloon_pop_prefabs/pop_4",
        "balloon_pop_prefabs/pop_5",
        "balloon_pop_prefabs/pop_6",
        "balloon_pop_prefabs/pop_7"
    };

    void OnMouseDown()
    {
        Debug.Log("Object clicked: " + gameObject.name);

        Destroy(gameObject);

        System.Random random = new System.Random();
        int randomIndex = random.Next(prefabsPaths.Count);
        string prefabPath = prefabsPaths[randomIndex];

        GameObject particlePrefab = Resources.Load<GameObject>(prefabPath);
        
        GameObject particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}