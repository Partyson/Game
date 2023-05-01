using System.Drawing;

namespace Game.Model.Entity
{
    public class Entity
    {
        public Point Position { get; protected set; }
        public int Health { get; protected set; }
        
        public int Damage { get; protected set; }

        public Entity(int x, int y)
        {
            Position = new Point(x, y);

        }
        public Entity(int x, int y, int health, int damage)
        {
            Position = new Point(x, y);
            Health = health;
            Damage = damage;
        }
    }
}