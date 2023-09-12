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
        
        private Dictionary<string, Sprite> balloonsSprites;

        private Random rand = new Random();

        private async void initViewModel()
        {
            balloonsSprites = await BalloonsLoader.instance.loadBalloons();
            // инициализация шаров. Надо бы вынести инициализацию на сплэш чтобы тут уже были готовые спрайты
        }

        public IObservable<Sprite> startGame()
        {
            return Observable
                .Interval(new TimeSpan(0, 0, 0, 2))
                .Select(x => balloonsSprites.ElementAt(rand.Next(0, balloonsSprites.Count)).Value);
        }

        public void stopGame()
        {
            // todo
        }
    }
}

