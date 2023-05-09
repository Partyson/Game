using System;

namespace Game.Model.EntityModel
{
    public class Enemy : EntityModel.Entity
    {
        public Enemy(int x, int y, Action onEnemyDied, int health = 100, int damage = 20) : base(x, y, onEnemyDied, health, damage)
        {
        }
        
    }
}