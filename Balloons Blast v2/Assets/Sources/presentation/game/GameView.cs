using System.Collections;
using UniRx;
using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        private GameViewModel viewModel;

        private void Start()
        {
            viewModel = new GameViewModel();

            viewModel.startGame()
                .Subscribe(balloonSprite =>
                {
                    StartCoroutine(
                        MoveBalloon(
                            CreateBalloon(balloonSprite)
                            )
                        );
                });
        }
        
        // todo Добавить переиспользование шаров. Создать пул шаров
        private GameObject CreateBalloon(Sprite balloonSprite)
        {
            var gameObj = new GameObject();
            var spriteRenderer = gameObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = balloonSprite;
            return gameObj;
        }

        /*
         * todo
         * 1. Сделать движение шара и его уничтожение после выхода за границы экрана
         */
        private IEnumerator MoveBalloon(GameObject obj)
        {
            var count = 0;
            while (count < 20)
            {
                print("Iteration");
                obj.transform.Translate(
                    new Vector3(0, 0.2f, 0)
                );
                count = +1;
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }
    }
}
