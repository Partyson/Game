using System.Drawing;

namespace Game
{
    public static class GameSettings
    {
        public const int MapHeight = 1000;
        public const int MapWidth = 1000;
        public const int TileSize = 32;
        public static readonly Size ViewportSize = new Size(2000, 1000);
        public static readonly Rectangle MinRectangleSpawn = new Rectangle(new Point(-ViewportSize.Width /2 , -ViewportSize.Height /2), ViewportSize);
        public static readonly Rectangle MaxRectangleSpawn = new Rectangle(new Point(-ViewportSize.Width /2 - 300, -ViewportSize.Height / 2 - 300), ViewportSize + new Size(600, 600));
    }
}