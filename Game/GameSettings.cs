using System.Drawing;

namespace Game
{
    public static class GameSettings
    {
        public const int MapHeight = 1000;
        public const int MapWidth = 1000;
        public const int TileSize = 32;
        public static readonly Rectangle MinRectangleSpawn = new Rectangle(new Point(100, 100), new Size(200, 100));
        public static readonly Rectangle MaxRectangleSpawn = new Rectangle(new Point(50, 50), new Size(500, 250));

        public const int MaxRadiusSpawn = 300;
    }
}