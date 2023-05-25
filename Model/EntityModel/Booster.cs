using System;

namespace Game.Model.EntityModel
{
    public class Booster : Entity
    {
        public BoosterData BoosterData { get;}
        public Booster(int x, int y, Action<Entity> onBoosterOnEntityDied, BoosterData boosterData) : base(x, y, onBoosterOnEntityDied, 1, 0)
        {
            BoosterData = boosterData;
        }
    }
}