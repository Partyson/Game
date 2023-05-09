using System;
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
        private int _counter;
        private object _lockObject = new object();
        private readonly FastNoise _fastNoise = new FastNoise();
        private Timer _timer = new Timer();
        private CollisionController _collisionController;
        private EnemySpawner _enemySpawner;
        private EnemyAi _enemyAi;

        public GameScreen(GameModel gameModel) : base(gameModel)
        {
            _collisionController = new CollisionController(gameModel);
            _enemySpawner = new EnemySpawner(gameModel);
            _enemyAi = new EnemyAi(gameModel);
            _fastNoise.Frequency = 0.1f;
            
            _timer.Interval = 100;
            _timer.Elapsed += UpdateGameState;
            _timer.Start();
            
            gameModel.StartGame();
        }

        private void UpdateGameState(object sender, ElapsedEventArgs e)
        {
            lock(_lockObject)
            {
                SpawnEnemy();
                _enemyAi.Update();

                if (!_collisionController.TryGetPlayerCollision(out var entity))
                    return;
                if (entity is Booster)
                    OnBoosterCollied(entity as Booster);
                else
                    OnEnemyCollied(entity as Enemy);

                _counter++;
            }
        }

        private void SpawnEnemy()
        {
            if(_counter % 2 == 0)
                _enemySpawner.Spawn();
        }

        private void OnBoosterCollied(Booster booster)
        {
            booster.GetDamage(booster.Health);
            _gameModel.Player.TakeBooster(booster);
        }

        private void OnEnemyCollied(Enemy enemy) => _gameModel.Player.GetDamage(enemy.Damage);
        
        protected override void OnPaint(PaintEventArgs e)
        {
            lock(_lockObject)
            {
                DrawMap(e.Graphics);
                DrawPlayer(e.Graphics);
                DrawEnemies(e.Graphics);
                Invalidate();

                base.OnPaint(e);
            }
        }

        private void DrawEnemies(Graphics graphics)
        {
            foreach (var enemy in _gameModel.Enemies)
            {
                var enemyViewportPosition = ConvertToViewportPosition(enemy.Position);
                graphics.FillEllipse(new SolidBrush(Color.Black), enemyViewportPosition.X - 8, enemyViewportPosition.Y - 8, 16,
                    16);
            }
        }

        private void DrawPlayer(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.White), ClientSize.Width / 2 - 8, ClientSize.Height / 2 - 8, 16, 16);
        }

        private Point ConvertToViewportPosition(Point position)
        {
            var playerPosition = _gameModel.Player.Position;
            return new Point(position.X - playerPosition.X + ClientSize.Width / 2,
                position.Y - playerPosition.Y + ClientSize.Height / 2);
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