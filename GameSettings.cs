using System.Drawing;

namespace Game
{
    public static class GameSettings
    {
        public const int TileSize = 32;
        public static readonly Size ViewportSize = new Size(2000, 1000);

        public static readonly Rectangle MinSpawnRange =
            new Rectangle(new Point(
                    -ViewportSize.Width / 2,
                    -ViewportSize.Height / 2),
                    ViewportSize);

        public static readonly Rectangle MaxSpawnRange =
            new Rectangle(new Point(
                    ViewportSize.Width / 2 - 300,
                    -ViewportSize.Height / 2 - 300),
                ViewportSize + new Size(600, 600));
        
        public static readonly Rectangle MaxBoosterSpawnRange = 
            new Rectangle(new Point(
                ViewportSize.Width / 2 - 900,
                -ViewportSize.Height / 2 - 900),
            ViewportSize + new Size(1800, 1800));
    }
}