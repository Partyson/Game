using System;
using System.Drawing;

namespace Game.Model.Entity
{
    public class Player : Entity
    {
        public event Action PlayerDied;

        public int BulletLimit { get; protected set; }
        public int MaxHealth { get; protected set; }
        

        public Player(int x, int y, int health = 100, int damage = 20) : base(x, y, health, damage)
        {
        }

        public void Move(Size offset) => Position += offset;

        public void GetDamage(int takenDamage)
        {
            Health -= takenDamage;
            if (Health <= 0)
                PlayerDied?.Invoke();
        }

        public void TakeBooster(Booster booster)
        {
            switch (booster.BoosterData.Type)
            {
                //TODO: пиши свич
            }
        }
    }
}