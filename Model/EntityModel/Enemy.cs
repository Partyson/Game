using System;

namespace Game.Model.EntityModel
{
    public class Enemy : Entity
    {
        public Enemy(int x, int y, Action<Entity> onEnemyDied, int health, int damage) : base(x, y, onEnemyDied, health, damage)
        {
        }
        
    }
}