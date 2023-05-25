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
        
        public Image Tutorial { get; }


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
            Tutorial = LoadImageFromAssets("tutorial.png");
        }
    }
}