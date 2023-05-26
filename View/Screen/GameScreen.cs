using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private static readonly Images Images = new Images();

        private readonly Dictionary<BoosterType, Image> _colors = new Dictionary<BoosterType, Image>
        {
            { BoosterType.Damage, Images.Damage },
            { BoosterType.HeathRegeneration, Images.HpRegen },
            { BoosterType.MaxHealth, Images.MaxHealth },
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

            _gameTickController.RegisterAction(SpawnEnemy, 10);
            _gameTickController.RegisterAction(SpawnBooster, 15);
            _gameTickController.RegisterAction(UpdateEnemyAi, 1);
            _gameTickController.RegisterAction(CheckCollision, 1);
            _gameTickController.RegisterAction(Regeneration, 1);
            _gameTickController.RegisterAction(UpEnemy, 100);
            _gameTickController.StartTimer();
            GameModel.GameStateChanged += GameModelOnGameStateChanged;

            gameModel.StartGame();
        }

        private void Regeneration()
        {
            GameModel.Player.Regeneration();
        }

        private void UpEnemy()
        {
            GameModel.CurrentEnemyDamage *= 1.15;
            GameModel.CurrentEnemyHealth *= 1.15;
        }

        private void GameModelOnGameStateChanged(GameState gameState)
        {
            if (gameState != GameState.Game)
                _gameTickController.StopTimer();
        }

        private void SpawnEnemy()
        {
            lock (_lockObject)
                _enemySpawner.Spawn();
        }

        private void UpdateEnemyAi()
        {
            lock (_lockObject)
                _enemyAi.Update();
        }

        private void SpawnBooster()
        {
            lock (_lockObject)
                _boosterSpawner.Spawn();
        }

        private void CheckCollision()
        {
            lock (_lockObject)
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
            GameModel.Player.TakeBooster(booster);
        }

        private void OnEnemyCollied(Enemy enemy) => GameModel.Player.GetDamage(enemy.Damage);

        protected override void OnMouseClick(MouseEventArgs eventArgs)
        {
            lock (_lockObject)
            {
                foreach (var enemy in from enemy in GameModel.Enemies
                         let mouseWorldSystemPosition = ConvertToWorldSystem(eventArgs.Location)
                         where enemy.HitBox.Contains(mouseWorldSystemPosition)
                         select enemy)
                {
                    enemy.GetDamage(GameModel.Player.Damage);
                    break;
                }

                base.OnMouseClick(eventArgs);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (_lockObject)
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
            foreach (var booster in GameModel.Boosters)
            {
                var boosterViewportPosition = ConvertToViewportSystem(booster.Position);
                var hitbox = (booster.HitBox.Size.Width, booster.HitBox.Size.Height);
                graphics.DrawImage(_colors[booster.BoosterData.Type],
                    boosterViewportPosition.X - hitbox.Width / 2,
                    boosterViewportPosition.Y - hitbox.Height / 2, hitbox.Width, hitbox.Height);
            }
        }

        private void DrawEnemies(Graphics graphics)
        {
            foreach (var enemy in GameModel.Enemies)
            {
                var hitbox = (enemy.HitBox.Size.Width, enemy.HitBox.Size.Height);
                var enemyViewportPosition = ConvertToViewportSystem(enemy.Position);
                var health = (int)enemy.Health;
                var brush = new SolidBrush(Color.Black);
                graphics.DrawString(health.ToString(), SystemFonts.CaptionFont, brush, enemyViewportPosition + new Size(hitbox.Width, 0));
                graphics.FillEllipse(brush, enemyViewportPosition.X, enemyViewportPosition.Y, hitbox.Width, hitbox.Height);
            }
        }

        private void DrawPlayer(Graphics graphics)
        {
            var hitbox = (GameModel.Player.HitBox.Size.Width, GameModel.Player.HitBox.Size.Height);
            var playerViewportPosition = ConvertToViewportSystem(GameModel.Player.Position);
            var health = (int)GameModel.Player.Health;

            graphics.FillEllipse(new SolidBrush(Color.White), ClientSize.Width / 2 - 8, ClientSize.Height / 2 - 8,
                GameModel.Player.HitBox.Size.Width, GameModel.Player.HitBox.Size.Height);
            graphics.DrawString(health.ToString(), SystemFonts.CaptionFont,
                new SolidBrush(Color.Black),playerViewportPosition + new Size(hitbox.Width, 0));
        }

        private Point ConvertToViewportSystem(Point position)
        {
            var playerPosition = GameModel.Player.Position;
            return new Point(position.X - playerPosition.X + ClientSize.Width / 2,
                position.Y - playerPosition.Y + ClientSize.Height / 2);
        }

        private Point ConvertToWorldSystem(Point position)
        {
            var playerPosition = GameModel.Player.Position;
            return new Point(position.X + playerPosition.X - ClientSize.Width / 2,
                position.Y + playerPosition.Y - ClientSize.Height / 2);
        }

        private void DrawMap(Graphics graphics)
        {
            var clientHalfWidth = ClientSize.Width / 2;
            var clientHalfHeight = ClientSize.Height / 2;
            var startX = -GameModel.Player.Position.X % GameSettings.TileSize;
            var startY = -GameModel.Player.Position.Y % GameSettings.TileSize;

            var startColumn = ConvertToTilesSystem(GameModel.Player.Position.X) -
                              ConvertToTilesSystem(clientHalfWidth);
            var startRow = ConvertToTilesSystem(GameModel.Player.Position.Y) - ConvertToTilesSystem(clientHalfHeight);

            for (var column = startColumn - 1;
                 column <= startColumn + ConvertToTilesSystem(ClientSize.Width) + 1;
                 column++)
            for (var row = startRow - 1; row <= startRow + ConvertToTilesSystem(ClientSize.Height) + 1; row++)
            {
                var x = ConvertToPixelsSystem(column - startColumn) + startX;
                var y = ConvertToPixelsSystem(row - startRow) + startY;

                var color = (int)((_fastNoise.GetPerlin(column, row) + 1) / 2 * 255);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(color, color, color)), x, y, GameSettings.TileSize,
                    GameSettings.TileSize);
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

        private void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                case Keys.Left:
                    GameModel.Player.Move(new Size(-10, 0));
                    break;
                case Keys.D:
                case Keys.Right:
                    GameModel.Player.Move(new Size(10, 0));
                    break;
                case Keys.W:
                case Keys.Up:
                    GameModel.Player.Move(new Size(0, -10));
                    break;
                case Keys.S:
                case Keys.Down:
                    GameModel.Player.Move(new Size(0, 10));
                    break;
            }

            Invalidate();
        }
    }
}