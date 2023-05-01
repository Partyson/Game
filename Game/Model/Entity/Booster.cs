﻿namespace Game.Model.Entity
{
    public class Booster : Entity
    {
        public BoosterData BoosterData { get;}
        public Booster(int x, int y, BoosterData boosterData) : base(x, y)
        {
            BoosterData = boosterData;
        }
        
    }
}