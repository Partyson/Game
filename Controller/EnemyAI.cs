using System;
using System.Drawing;
using Game.Model;

namespace Game.Controller
{
    public class EnemyAi
    {
        private readonly GameModel _gameModel;

        public EnemyAi(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void Update()
        {
            var playerPosition = _gameModel.Player.Position;
            foreach (var enemy in _gameModel.Enemies)
            {
                var vector = new Size(playerPosition.X - enemy.Position.X, playerPosition.Y - enemy.Position.Y);
                var length = GetLength(vector);
                if(length >= 1)
                    enemy.Move(Normalize(vector));
            }
        }
        

        private static Size Normalize(Size vector)
        {
            var length = GetLength(vector);
            return new Size((int)(6 * vector.Width / length), (int)(6 * vector.Height / length));
        }
        
        private static double GetLength(Size vector) => Math.Sqrt(vector.Height * vector.Height + vector.Width * vector.Width);
        
        
    }
}