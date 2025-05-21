using UnityEngine;
using System.Collections.Generic;

public class BalloonClickHandler : MonoBehaviour {

    public double balloonSpeed = 0.0;

    void OnMouseDown()
    {
        // Удаляю шар
        Destroy(gameObject);

        GameScoreManager.UpdateScore(balloonSpeed);

        // Забираю из ресурсов анимацию взрыва с навешенным на нее компонентом Explosion.cs
        GameObject prefab = ExplosionLoader.GetExplosion(gameObject);

        // Создаю копию префаба на месте удаленного шара
        Instantiate(
            prefab,
            transform.position,
            Quaternion.identity
            );
    }
}