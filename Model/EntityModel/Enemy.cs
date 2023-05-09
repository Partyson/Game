using System;

namespace Game.Model.EntityModel
{
    public class Enemy : Entity
    {
        public Enemy(int x, int y, Action<Entity> onEnemyDied, int health = 100, int damage = 20) : base(x, y, onEnemyDied, health, damage)
        {
        }
        
    }
}