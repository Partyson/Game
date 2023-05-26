using System;
using System.Collections.Generic;
using Game.Model;
using Game.Model.EntityModel;

namespace Game.Controller
{
    public class EnemySpawner
    {
        private readonly GameModel _gameModel;
        private readonly Random _random = new Random();

        private readonly Dictionary<Side, (int minX, int maxX, int minY, int maxY)> _spawnConfines =
            new Dictionary<Side, (int, int, int, int)>
            {
                {
                    Side.Top,
                    (GameSettings.MaxSpawnRange.Left, GameSettings.MaxSpawnRange.Right,
                        GameSettings.MaxSpawnRange.Top, GameSettings.MinSpawnRange.Top)
                },
                {
                    Side.Bottom,
                    (GameSettings.MaxSpawnRange.Left, GameSettings.MaxSpawnRange.Right,
                        GameSettings.MinSpawnRange.Bottom, GameSettings.MaxSpawnRange.Bottom)
                },
                {
                    Side.Left,
                    (GameSettings.MaxSpawnRange.Left,  GameSettings.MinSpawnRange.Left,
                        GameSettings.MaxSpawnRange.Top, GameSettings.MaxSpawnRange.Bottom)
                },
                {
                    Side.Right,
                    (GameSettings.MinSpawnRange.Right, GameSettings.MaxSpawnRange.Right,
                        GameSettings.MaxSpawnRange.Top, GameSettings.MaxSpawnRange.Bottom)
                }
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