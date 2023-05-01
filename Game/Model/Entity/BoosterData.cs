using System.Drawing;

namespace Game.Model.Entity
{
    public class BoosterData
    {
        public BoosterType Type { get; }
        public int Value { get; }

        public Image Image { get; }

        public BoosterData(BoosterType type, int value, Image image)
        {
            Type = type;
            Value = value;
            Image = image;
        }
    }
}