using Sources.data;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngineRandom = UnityEngine.Random;

namespace Game
{
    public class GameViewModel
    {
        public GameViewModel(){}
        
        private IDisposable balloonsSpawnerDisposable = null;

        public float Delay = 1f;

        private Subject<Unit> _trigger = new Subject<Unit>();

        private bool isBalloonsBoostEnabled = false;

        public IObservable<Sprite> startGame()
        {
            return Observable.Create<Sprite>(observer =>
             {
                 return _trigger
                 .StartWith(Unit.Default)
                 .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(getBalloobsCreationDelay())))
                 .Subscribe(_ =>
                      {
                           observer.OnNext(BalloonsLoader.getRandomBalloonSprite());
                           _trigger.OnNext(Unit.Default);
                      }
                  );
             });

        }

        public void activateBalloonsBoost()
        {
            isBalloonsBoostEnabled = true;
        }

        public void diactivateBalloonsBoost()
        {
            isBalloonsBoostEnabled = false;
        }

        private float getBalloobsCreationDelay()
        {
            if (isBalloonsBoostEnabled)
            {
                return UnityEngine.Random.Range(0.8f, 1f);
            }
            else
            {
                return 0.1f;
            }
        }

        public void addBalloonsSpawnerDisposable(IDisposable disposable)
        {
            balloonsSpawnerDisposable = disposable;
        }

        public void stopGame()
        {
            balloonsSpawnerDisposable?.Dispose();
            diactivateBalloonsBoost();
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

