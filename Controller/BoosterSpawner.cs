using System;
using System.Collections.Generic;
using Game.Model;
using Game.Model.EntityModel;

namespace Game.Controller
{
    public class BoosterSpawner
    {
        private readonly GameModel _gameModel;
        private readonly Random _random = new Random();

        private List<BoosterData> _boosterDatas = new List<BoosterData>()
        {
            new BoosterData(BoosterType.Damage, 0.1),
            new BoosterData(BoosterType.HeathRegeneration, 0.2),
            new BoosterData(BoosterType.MaxHealth, 0.1),
            new BoosterData(BoosterType.ReloadSpeed, 0.1),
            new BoosterData(BoosterType.BulletLimit, 1)
        };

        public BoosterSpawner(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Spawn()
        {
            var x = _random.Next(GameSettings.MaxBoosterSpawnRange.Width) + _gameModel.Player.Position.X;
            var y = _random.Next(GameSettings.MaxBoosterSpawnRange.Height) + _gameModel.Player.Position.Y;
            var randomBoosterData = _boosterDatas[_random.Next(4)];
            _gameModel.SpawnBooster(x, y, randomBoosterData);

        }
    }
}