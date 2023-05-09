using System.Drawing;

namespace Game.Model.EntityModel
{
    public class BoosterData
    {
        public BoosterType Type { get; }
        public double Value { get; }

        public Image Image { get; }

        public BoosterData(BoosterType type, double value, Image image)
        {
            Type = type;
            Value = value;
            Image = image;
        }
    }
}