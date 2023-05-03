using System;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using DotnetNoise;
using Game.Assets.Images;
using Game.Controller;
using Game.Model;
using Game.Model.EntityModel;
using Timer = System.Timers.Timer;

namespace Game.View.Screen
{
    public class GameScreen : BaseScreen
    {
        private readonly Images _images = new Images();
        private readonly FastNoise _fastNoise = new FastNoise();
        private Timer _timer = new Timer();
        private CollisionController _collisionController;

        public GameScreen(GameModel gameModel) : base(gameModel)
        {
            gameModel.StartGame();
            _collisionController = new CollisionController(gameModel);
            _fastNoise.Frequency = 0.1f;
            _timer.Interval = 100;
            _timer.Elapsed += UpdateGameState;
            _timer.Start();
        }

        private void UpdateGameState(object sender, ElapsedEventArgs e)
        {
            if(!_collisionController.TryGetPlayerCollision(out var entity))
                return;
            if (entity is Booster)
                OnBoosterCollied(entity as Booster);
            else
                OnEnemyCollied(entity as Enemy);
        }

        private void OnBoosterCollied(Booster booster)
        {
            booster.GetDamage(booster.Health);
            _gameModel.Player.TakeBooster(booster);
        }

        private void OnEnemyCollied(Enemy enemy) => _gameModel.Player.GetDamage(enemy.Damage);

        
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawMap(e.Graphics);
            DrawPlayer(e.Graphics);
            Invalidate();
            
            base.OnPaint(e);
        }

        private void DrawPlayer(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Red), ClientSize.Width / 2 - 8, ClientSize.Height / 2 - 8, 16,
                16);
        }

        private void DrawMap(Graphics graphics)
        {
            var clientHalfWidth = ClientSize.Width / 2;
            var clientHalfHeight = ClientSize.Height / 2;
            var startX = -_gameModel.Player.Position.X % GameSettings.TileSize;
            var startY = -_gameModel.Player.Position.Y % GameSettings.TileSize;

            var startColumn = ConvertToTilesSystem(_gameModel.Player.Position.X) - ConvertToTilesSystem(clientHalfWidth);
            var startRow = ConvertToTilesSystem(_gameModel.Player.Position.Y) - ConvertToTilesSystem(clientHalfHeight);

            for (var column = startColumn - 1; column <= startColumn + ConvertToTilesSystem(ClientSize.Width) + 1; column++)
            for (var row = startRow - 1; row <= startRow + ConvertToTilesSystem(ClientSize.Height) + 1; row++)
            {
                var x = ConvertToPixelsSystem(column - startColumn) + startX;
                var y = ConvertToPixelsSystem(row - startRow) + startY;
                
                var color = (int) ((_fastNoise.GetPerlin(column, row) + 1) / 2 * 255);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(color, color, color)), x, y, GameSettings.TileSize, GameSettings.TileSize);
            }
            
        }
    
        private int ConvertToTilesSystem(int value) => value / GameSettings.TileSize;

        private int ConvertToPixelsSystem(int value) => value * GameSettings.TileSize;
        
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
            base.OnResize(e);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            KeyDown(e.KeyCode);
            base.OnKeyDown(e);
        }

        public void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                case Keys.Left:
                    _gameModel.Player.Move(new Size(-10, 0));
                    break;
                case Keys.D:
                case Keys.Right:
                    _gameModel.Player.Move(new Size(10, 0));
                    break;
                case Keys.W:
                case Keys.Up:
                    _gameModel.Player.Move(new Size(0, -10));
                    break;
                case Keys.S:
                case Keys.Down:
                    _gameModel.Player.Move(new Size(0, 10));
                    break;
            }

            Invalidate();
        }
    }
}