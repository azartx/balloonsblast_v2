using System;
using UniRx;
using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        private GameViewModel viewModel;

        public IObservable<Sprite> balloonsStream;

        private void Start()
        {
            viewModel = new GameViewModel();

            viewModel.startGame()
                .Subscribe(balloonSprite =>
                {
                    Debug.Log(balloonSprite.name);
                });
        }
    }
}


