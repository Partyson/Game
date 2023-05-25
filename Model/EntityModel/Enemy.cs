using System;

namespace Game.Model.EntityModel
{
    public class Enemy : Entity
    {
        public Enemy(int x, int y, Action<Entity> onEnemyOnEntityDied, int health, int damage) : base(x, y, onEnemyOnEntityDied, health, damage)
        {
        }
        
    }
}