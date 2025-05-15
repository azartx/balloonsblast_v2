using System;
using System.Collections.Generic;
using System.Linq;
using Sources.data;
using UniRx;
using UnityEngine;
using UnityEngineRandom = UnityEngine.Random;

namespace Game
{
    public class GameViewModel
    {
        public GameViewModel(){}
        
        private IDisposable balloonsSpawnerDisposable = null;

        public IObservable<Sprite> startGame()
        {
            return Observable
                // Задает фиксированный интервал - шар вылетает раз в две секунды
                // TODO: добавить вылет шара в промежутке времени
                .Interval(new TimeSpan(0, 0, 0, 2))
                .Select(x => BalloonsLoader.getRandomBalloonSprite());
        }

        public void addBalloonsSpawnerDisposable(IDisposable disposable)
        {
            balloonsSpawnerDisposable = disposable;
        }

        public void stopGame()
        {
            balloonsSpawnerDisposable?.Dispose();
        }

        public Vector3 getRandomBottomPoint(float defaultZ, Vector3 balloonSize)
        {
            Vector3 screenBottomLeft = Camera.main
                .ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 screenTopRight = Camera.main
                .ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            float screenWidth = screenTopRight.x - screenBottomLeft.x;
            float screenHeight = screenTopRight.y - screenBottomLeft.y;

            // Выбираем случайное место внутри нижней части экрана
            float randomX = UnityEngineRandom.Range(screenBottomLeft.x + balloonSize.x, screenTopRight.x - balloonSize.x);
            float randomY = screenBottomLeft.y - balloonSize.y;

            return new Vector3(randomX, randomY, defaultZ);
        }
    }
}

