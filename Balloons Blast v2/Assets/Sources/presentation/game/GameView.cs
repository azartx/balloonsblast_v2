using System.Collections;
using UniRx;
using UnityEngine;
using Random = System.Random;

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
            var gameObj = new GameObject();
            var spriteRenderer = gameObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = balloonSprite;
            return gameObj;
        }

        private IEnumerator MoveBalloon(GameObject obj)
        {
            placeBalloonOnRandomBottomPoint(obj);

            float speed = UnityEngine.Random.Range(0.05f, 0.2f);

            var count = 0;
            while (count < 20)
            {
                print("Iteration");
                obj.transform.Translate(
                    new Vector3(0, speed, 0)
                );
                count = +1;

                if (obj.transform.position.y > screenTopRight.Value.y)
                {
                    Debug.LogError("Объект вышел за границы, удаление");
                    Destroy(obj);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }

        private void placeBalloonOnRandomBottomPoint(GameObject obj)
        {
            // Перемещаем объект в выбранное место
            obj.transform.position = viewModel.getRandomBottomPoint(obj.transform.position.z);
        }
    }
}
