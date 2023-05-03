using System;
using System.Drawing;

namespace Game.Model.EntityModel
{
    public abstract class Entity
    {
        public Point Position { get; protected set; }
        public double Health { get; protected set; }
        
        public double Damage { get; protected set; }
        private event Action EntityDied;

        public Rectangle HitBox { get; protected set; }
        

        public Entity(int x, int y)
        {
            Position = new Point(x, y);

        }
        public Entity(int x, int y, Action onEntityDied, double health, double damage)
        {
            Position = new Point(x, y);
            Health = health;
            Damage = damage;
            EntityDied += onEntityDied;
        }
        
        public void Move(Size offset) => Position += offset;

        public void GetDamage(double takenDamage)
        {
            Health -= takenDamage;
            if (Health <= 0)
                EntityDied();
        }
    }
}