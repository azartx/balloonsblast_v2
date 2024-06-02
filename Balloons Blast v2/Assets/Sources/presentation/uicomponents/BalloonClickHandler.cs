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
        // Удаляю шар
        Destroy(gameObject);

        // Забираю из ресурсов анимацию  взрыва
        GameObject prefab = ExplosionLoader.GetExplosion(gameObject);

        // Создаю копию префаба на месте удаленного шара
        GameObject explosion = Instantiate(
            prefab,
            transform.position,
            Quaternion.identity
            );
        
        // запускаю анимацию префаба (взрыв)
        explosion.GetComponent<ParticleSystem>().Play();

        // Добавляем компонент аудио для проигрыша звука взрыва и запускаем
        AudioSource clap = explosion.AddComponent<AudioSource>();
        clap.clip = AudioLoader.RandomClap();
        clap.Play();
    }
}