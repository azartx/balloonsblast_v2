using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool isExplosionAnimationStarted = false;
    private ParticleSystem particle = null;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Play();
        isExplosionAnimationStarted = true;

        // Добавляем компонент аудио для проигрыша звука взрыва и запускаем
        AudioSource clap = gameObject.AddComponent<AudioSource>();
        clap.clip = AudioLoader.RandomClap();
        clap.Play();
    }

    void Update()
    {
        if (particle != null && isExplosionAnimationStarted == true && !particle.IsAlive(true))
        {
            particle = null;
            Destroy(gameObject);
        }
    }
}