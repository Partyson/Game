using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DotnetNoise;
using Game.Assets.Images;
using Game.Controller;
using Game.Model;
using Game.Model.EntityModel;

namespace Game.View.Screen
{
    public class GameScreen : BaseScreen
    {
        private readonly Images _images = new Images();

        private readonly Dictionary<BoosterType, Color> _colors = new Dictionary<BoosterType, Color>
        {
            { BoosterType.Damage, Color.Aqua },
            { BoosterType.BulletLimit, Color.Orange },
            { BoosterType.HeathRegeneration, Color.Green },
            { BoosterType.MaxHealth, Color.Red },
            { BoosterType.ReloadSpeed, Color.Blue }
        };
        private readonly object _lockObject = new object();
        private readonly FastNoise _fastNoise = new FastNoise();
        private readonly CollisionController _collisionController;
        private readonly BoosterSpawner _boosterSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly EnemyAi _enemyAi;
        private readonly GameTickController _gameTickController;

        public GameScreen(GameModel gameModel) : base(gameModel)
        {

            _gameTickController = new GameTickController(100);
            _collisionController = new CollisionController(gameModel);
            _enemySpawner = new EnemySpawner(gameModel);
            _boosterSpawner = new BoosterSpawner(gameModel);
            _enemyAi = new EnemyAi(gameModel);
            _fastNoise.Frequency = 0.1f;
            
            _gameTickController.RegisterAction(SpawnEnemy, 5);
            _gameTickController.RegisterAction(SpawnBooster, 10);
            _gameTickController.RegisterAction(UpdateEnemyAi, 1);
            _gameTickController.RegisterAction(CheckCollision, 1);
            _gameTickController.StartTimer();
            _gameModel.GameStateChanged += GameModelOnGameStateChanged;
            gameModel.StartGame();
        }

        private void GameModelOnGameStateChanged(GameState gameState)
        {
            Console.Write("afasfasfasf");
            if(gameState == GameState.Game)
                _gameTickController.StopTimer();
        }

        private void SpawnEnemy()
        {
            lock(_lockObject)
                _enemySpawner.Spawn();
        }

        private void UpdateEnemyAi()
        {
            lock(_lockObject)
                _enemyAi.Update();
        }

        private void SpawnBooster()
        {
            lock(_lockObject)
                _boosterSpawner.Spawn();
        }
        
        private void CheckCollision()
        {
            lock(_lockObject)
            {
                if (!_collisionController.TryGetPlayerCollision(out var entity))
                    return;
                if (entity is Booster)
                    OnBoosterCollied(entity as Booster);
                else
                    OnEnemyCollied(entity as Enemy);
            }
        }

        private void OnBoosterCollied(Booster booster)
        {
            booster.GetDamage(booster.Health);
            _gameModel.Player.TakeBooster(booster);
        }

        private void OnEnemyCollied(Enemy enemy) => _gameModel.Player.GetDamage(enemy.Damage);

        protected override void OnMouseClick(MouseEventArgs e)
        {
            lock(_lockObject)
            {
                foreach (var enemy in _gameModel.Enemies)
                {
                    var mouseWorldSystemPosition = ConvertToWorldSystem(e.Location);
                    
                    var mouseHitBox = new HitBox(
                        new Point(mouseWorldSystemPosition.X - GameSettings.AttackArea,
                            mouseWorldSystemPosition.Y - GameSettings.AttackArea),
                        new Size(GameSettings.AttackArea * 2, GameSettings.AttackArea * 2));
                    
                    if (enemy.HitBox.IsIntersect(mouseHitBox))
                    {
                        enemy.GetDamage(_gameModel.Player.Damage);
                        break;
                    }
                }

                base.OnMouseClick(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock(_lockObject)
            {
                DrawMap(e.Graphics);
                DrawPlayer(e.Graphics);
                DrawEnemies(e.Graphics);
                DrawBoosters(e.Graphics);
                Invalidate();

                base.OnPaint(e);
            }
        }

        private void DrawBoosters(Graphics graphics)
        {
            foreach (var booster in _gameModel.Boosters)
            {
                var boosterViewportPosition = ConvertToViewportSystem(booster.Position);
                var hitbox = (booster.HitBox.Size.Width, booster.HitBox.Size.Height);
                graphics.FillRectangle(new SolidBrush(_colors[booster.BoosterData.Type]),boosterViewportPosition.X - hitbox.Width / 2,
                    boosterViewportPosition.Y - hitbox.Height / 2, hitbox.Width, hitbox.Height);
            }
        }
        private void DrawEnemies(Graphics graphics)
        {
            foreach (var enemy in _gameModel.Enemies)
            {
                var hitbox = (enemy.HitBox.Size.Width, enemy.HitBox.Size.Height);
                var enemyViewportPosition = ConvertToViewportSystem(enemy.Position);
                var hitboxViewportPosition = ConvertToViewportSystem(enemy.HitBox.Position);
                graphics.FillEllipse(new SolidBrush(Color.Black), enemyViewportPosition.X - hitbox.Width / 2,
                    enemyViewportPosition.Y - hitbox.Height / 2, hitbox.Width, hitbox.Height);
                graphics.DrawRectangle(new Pen(Color.Chartreuse), hitboxViewportPosition.X - hitbox.Width / 2,
                    hitboxViewportPosition.Y - hitbox.Height / 2, hitbox.Width, hitbox.Height);
                
            }
        }

        private void DrawPlayer(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.White), ClientSize.Width / 2 - 8, ClientSize.Height / 2 - 8,
                _gameModel.Player.HitBox.Size.Width, _gameModel.Player.HitBox.Size.Height);
        }

        private Point ConvertToViewportSystem(Point position)
        {
            var playerPosition = _gameModel.Player.Position;
            return new Point(position.X - playerPosition.X + ClientSize.Width / 2,
                position.Y - playerPosition.Y + ClientSize.Height / 2);
        }

        private Point ConvertToWorldSystem(Point position)
        {
            var playerPosition = _gameModel.Player.Position;
            return new Point(position.X + playerPosition.X - ClientSize.Width / 2,
                position.Y + playerPosition.Y - ClientSize.Height / 2);
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