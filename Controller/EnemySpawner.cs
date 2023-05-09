using System;
using System.Collections.Generic;
using Game.Model;
using Game.Model.EntityModel;

namespace Game.Controller
{
    public class EnemySpawner
    {
        private readonly GameModel _gameModel;
        private Random _random = new Random();

        private Dictionary<Side, (int minX, int maxX, int minY, int maxY)> _spawnConfines =
            new Dictionary<Side, (int, int, int, int)>
            {
                {
                    Side.Top,
                    (GameSettings.MaxRectangleSpawn.Left, GameSettings.MaxRectangleSpawn.Right,
                        GameSettings.MaxRectangleSpawn.Top, GameSettings.MinRectangleSpawn.Top)
                },
                {
                    Side.Bottom,
                    (GameSettings.MaxRectangleSpawn.Left, GameSettings.MaxRectangleSpawn.Right,
                        GameSettings.MinRectangleSpawn.Bottom, GameSettings.MaxRectangleSpawn.Bottom)
                },
                {
                    Side.Left,
                    (GameSettings.MaxRectangleSpawn.Left, GameSettings.MinRectangleSpawn.Left,
                        GameSettings.MaxRectangleSpawn.Top, GameSettings.MaxRectangleSpawn.Bottom)
                },
                {
                    Side.Right,
                    (GameSettings.MinRectangleSpawn.Right, GameSettings.MaxRectangleSpawn.Right,
                        GameSettings.MaxRectangleSpawn.Top, GameSettings.MaxRectangleSpawn.Bottom)
                },
            };

        public EnemySpawner(GameModel gameModel)
        {
            _gameModel = gameModel;
        }
        
        public void Spawn()
        {
            var side = (Side)_random.Next(4);
            var playerPosition = _gameModel.Player.Position;
            
            var x = _random.Next(_spawnConfines[side].minX, _spawnConfines[side].maxX) + playerPosition.X;
            var y = _random.Next(_spawnConfines[side].minY, _spawnConfines[side].maxY) + playerPosition.Y;
            _gameModel.SpawnEnemy(x, y);
        }
    }
}