using System.Diagnostics;
using System.Drawing;
using Game.Model;

namespace Game.Assets.Images
{
    public class Images
    {
        public Image Red { get; }
        public Image Blue { get; }
        public Image Yellow { get; }
        public Image Green { get; }
        public Image White { get; }
        public Image Player { get; }


        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Images/" + fileName);

        public Images()
        {
            Red = LoadImageFromAssets("red.png");
            Blue = LoadImageFromAssets("blue.png");
            Yellow = LoadImageFromAssets("yellow.png");
            Green = LoadImageFromAssets("green.png");
            White = LoadImageFromAssets("white.png");
            Player = LoadImageFromAssets("player.png");
        }

        public Image GetImageFromType(LandType landType)
        {
            switch (landType)
            {
                case LandType.Blue:
                    return Blue;
                case LandType.Green:
                    return Green;
                case LandType.Red:
                    return Red;

                case LandType.Yellow:
                    return Yellow; 
                
            //   case LandType.White:
                default:
                    return White;
            }
        }
    }
}