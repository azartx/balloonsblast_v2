using System.Collections;
using UniRx;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        private GameViewModel viewModel;

        private System.Lazy<Vector3> screenTopRight;

        private void Start()
        {
            screenTopRight = new System.Lazy<Vector3>(() =>
                Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)));

            viewModel = new GameViewModel();

            viewModel.addBalloonsSpawnerDisposable
                (
                viewModel.startGame()
                    .Subscribe(balloonSprite =>
                    {
                        StartCoroutine(MoveBalloon(CreateBalloon(balloonSprite)));
                    })
                );
        }
        
        // todo Добавить переиспользование шаров. Создать пул шаров
        private GameObject CreateBalloon(Sprite balloonSprite)
        {
            // Создаём шар
            GameObject gameObj = new GameObject();

            gameObj.transform.SetParent(gameObject.transform);

            // вешаем модельку шара
            SpriteRenderer spriteRenderer = gameObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = balloonSprite;

            // увиличиваю размер шара в полтора раза
            spriteRenderer.transform.localScale =
                new Vector3(
                    gameObj.transform.localScale.x * 1.5f,
                    gameObj.transform.localScale.y * 1.5f,
                    gameObj.transform.localScale.z
                    );

            // вешаем коллайдер для того чтобы объект был физическим
            BoxCollider2D collider = gameObj.AddComponent<BoxCollider2D>();

            // Вешаем обработчик кликов, клики будут приходить в него
            gameObj.AddComponent<BalloonClickHandler>();

            return gameObj;
        }

        private IEnumerator MoveBalloon(GameObject obj)
        {
            placeBalloonOnRandomBottomPoint(obj);

            float speed = UnityEngine.Random.Range(0.009f, 0.018f);

            var count = 0;
            while (count < 20)
            {
                if (obj == null)
                {
                    print("Объект уже удалён, заканчиваю цикл");
                    yield break;
                }
                
                obj.transform.Translate(
                    new Vector3(0, speed, 0)
                );
                count = +1;

                if (obj.transform.position.y > screenTopRight.Value.y)
                {
                    print("Объект вышел за границы, удаление");
                    Destroy(obj);
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }

        private void placeBalloonOnRandomBottomPoint(GameObject obj)
        {
            // Перемещаем шар вниз экрана
            obj.transform.position = viewModel.getRandomBottomPoint(obj.transform.position.z);
        }
    }
}
