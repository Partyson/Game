using System.Drawing;

namespace Game.Model.EntityModel
{
    public class BoosterData
    {
        public BoosterType Type { get; }
        public double Value { get; }
        
        public BoosterData(BoosterType type, double value)
        {
            Type = type;
            Value = value;
        }
    }
}