using System;
using System.Drawing;

namespace Game.Model.EntityModel
{
    public abstract class Entity
    {
        public Point Position { get; protected set; }
        public double Health { get; protected set; }
        
        public double Damage { get; protected set; }
        private static event Action<Entity> EntityDied;

        public HitBox HitBox { get; protected set; }

        private static Action<Entity> _onEntityDied;
        

        public Entity(int x, int y)
        {
            Position = new Point(x, y);
            HitBox = new HitBox(Position, new Size(50, 50));
        }
        public Entity(int x, int y, Action<Entity> onEntityDied, double health, double damage)
        {
            Position = new Point(x, y);
            Health = health;
            Damage = damage;
            _onEntityDied = onEntityDied;
            EntityDied += onEntityDied;
            HitBox = new HitBox(Position, new Size(50, 50));
        }

        public void Move(Size offset)
        {
            Position += offset;
            HitBox.Position += offset;
        } 

        public void GetDamage(double takenDamage)
        {
            Health -= takenDamage;
            if (Health <= 0)
                EntityDied(this);
        }
        ~Entity()
        {
            EntityDied -= _onEntityDied;
        }
    }
}