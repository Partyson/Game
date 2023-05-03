using System;
using System.Collections.Generic;
using Game.Model;

namespace Game.Controller
{
    public class EnemySpawner
    {
        private readonly GameModel _gameModel;
        private Random _random = new Random();

        private Dictionary<Side, (int x, int y)> SpawnConfines = new Dictionary<Side, (int, int)>();


        public void Spawn()
        {
            var side = (Side)_random.Next(3);
            
        }
    }
}