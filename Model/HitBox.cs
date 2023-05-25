using System.Drawing;

namespace Game.Model.EntityModel
{
    public class HitBox
    {
        public Point Position { get; set; }
        
        public Size Size { get; }

        public HitBox(Point position, Size size)
        {
            Position = position;
            Size = size;
        }

        public bool IsIntersect(HitBox otherHitBox)
        {
            var otherPosition = otherHitBox.Position;
            var otherSize = otherHitBox.Size;
            return !(Position.X > otherPosition.X + otherSize.Width
                     || Position.X + Size.Width < otherPosition.X
                     || Position.Y > otherPosition.Y + otherSize.Height
                     || Position.Y + Size.Height < otherPosition.Y);
        }

        private bool Contains(int x, int y)
        {
            return Position.X <= x && x < Position.X + Size.Width && Position.Y <= y && y < Position.Y + Size.Height;
        }

        public bool Contains(Point point) => Contains(point.X, point.Y);
    }
}