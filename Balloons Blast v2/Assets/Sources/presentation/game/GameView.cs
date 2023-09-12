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
                    var gameObj = new GameObject();
                    var spriteRenderer = gameObj.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = balloonSprite;

                    var count = 0;
                    while (count < 20)
                    {
                        gameObj.transform.position = new Vector3(5, 5, 5);
                        count = + 1;
                    }
                });
        }
    }
}
