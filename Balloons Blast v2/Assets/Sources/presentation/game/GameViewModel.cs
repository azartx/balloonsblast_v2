using System;
using System.Collections.Generic;
using System.Linq;
using Sources.data;
using UniRx;
using UnityEngine;
using Random = System.Random;

namespace Game
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            initViewModel();
        }

        private IDisposable balloonsStreamDisposable;

        private Dictionary<string, Sprite> balloonsSprites;

        private Random rand = new Random();

        private async void initViewModel()
        {
            balloonsSprites = await BalloonsLoader.instance.loadBalloons();
            
           // startGame();
        }

        public IObservable<Sprite> startGame()
        {
            //balloonsStreamDisposable?.Dispose();
            return Observable
                .Interval(new TimeSpan(0, 0, 0, 2))
                .Select(x => balloonsSprites.ElementAt(rand.Next(0, balloonsSprites.Count)).Value);
            /*.Subscribe(str =>
            {
                Debug.Log("11111111111 : "+str);
            });*/
        }

        public void stopGame()
        {
            balloonsStreamDisposable?.Dispose();
            balloonsStreamDisposable = null;
        }
    }
}

