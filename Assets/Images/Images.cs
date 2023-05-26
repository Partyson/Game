using System.Diagnostics;
using System.Drawing;
using Game.Model;

namespace Game.Assets.Images
{
    public class Images
    {
        public Image Damage { get; }
        public Image HpRegen { get; }
        public Image MaxHealth { get; }
        public Image Tutorial { get; }


        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Images/" + fileName);

        public Images()
        {
            Damage = LoadImageFromAssets("gamage.png");
            HpRegen = LoadImageFromAssets("hpRegen.png");
            MaxHealth = LoadImageFromAssets("maxHealth.png");
            Tutorial = LoadImageFromAssets("tutorial.png");
        }
    }
}