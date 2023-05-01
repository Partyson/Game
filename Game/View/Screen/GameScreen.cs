using System;
using System.Drawing;
using System.Windows.Forms;
using Game.Assets.Images;
using Game.Model.Entity;

namespace Game.View.Screen
{
    public class GameScreen : BaseScreen
    {
        private readonly Images images = new Images();

        public GameScreen(GameModel gameModel) : base(gameModel)
        {
            gameModel.StartGame();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawMap(e.Graphics);
            Invalidate();
            base.OnPaint(e);
        }

        private void DrawMap(Graphics graphics)
        {
            var startPointInTiles = GetStartPointInTiles();
            Console.WriteLine(startPointInTiles);
            var clientWidthInTiles = ConvertPixelsToTiles(ClientSize.Width) + 1;
            var clientHeightInTiles = ConvertPixelsToTiles(ClientSize.Height) + 1;
            for (var x = startPointInTiles.X; x < startPointInTiles.X + clientWidthInTiles; x++)
            for (var y = startPointInTiles.Y; y < startPointInTiles.Y + clientHeightInTiles; y++)
            {
                //TODO вынести в переменные
                graphics.DrawImage(
                    _gameModel.Map.Landscape.ContainsKey(new Point(x, y))
                        ? images.GetImageFromType(_gameModel.Map.Landscape[new Point(x, y)])
                        : images.White, new Point(x * GameSettings.TileSize, y * GameSettings.TileSize));
            }
        }

        private Point GetStartPointInTiles()
        {
            var x = _gameModel.Player.Position.X - ClientSize.Width / 2;
            var y = _gameModel.Player.Position.Y - ClientSize.Height / 2;
            return new Point(ConvertPixelsToTiles(x), ConvertPixelsToTiles(y));
        }

        private int ConvertPixelsToTiles(int pixels) => (int)Math.Ceiling(pixels / (float)GameSettings.TileSize);

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
                    _gameModel.Player.Move(new Size(-1, 0));
                    break;
                case Keys.D:
                case Keys.Right:
                    _gameModel.Player.Move(new Size(1, 0));
                    break;
                case Keys.W:
                case Keys.Up:
                    _gameModel.Player.Move(new Size(0, 1));
                    break;
                case Keys.S:
                case Keys.Down:
                    _gameModel.Player.Move(new Size(0, -1));
                    break;
            }
        }
    }
}