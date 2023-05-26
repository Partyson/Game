using System;

namespace Game.Model.EntityModel
{
    public class Enemy : Entity
    {
        public Enemy(int x, int y, Action<Entity> onEnemyOnEntityDied, double health, double damage) : base(x, y, onEnemyOnEntityDied, health, damage)
        {
        }
        
    }
}