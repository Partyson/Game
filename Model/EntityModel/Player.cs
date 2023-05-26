using System;

namespace Game.Model.EntityModel
{
    public class Player : Entity
    {
        private int BulletLimit { get; set; }
        private double MaxHealth { get; set; }
        private double HeathRegeneration { get; set; }
        private double ReloadSpeed { get; set; }

        public Player(int x, int y, Action<Entity> onPlayerOnEntityDied, int health = 100, int damage = 20)
            : base(x, y, onPlayerOnEntityDied, health, damage)
        {
            MaxHealth = Health;
            HeathRegeneration = 1;
        }

        public void TakeBooster(Booster booster)
        {
            var offset = booster.BoosterData.Value;
            switch (booster.BoosterData.Type)
            {
                case BoosterType.Damage:
                    Damage *= offset;
                    break;
                case BoosterType.HeathRegeneration:
                    HeathRegeneration += offset;
                    break;
                case BoosterType.MaxHealth:
                    MaxHealth *= offset;
                    break;
            }
        }

        public void Regeneration()
        {
            if (Health + HeathRegeneration <= MaxHealth)
                Health += HeathRegeneration;
            else
                Health = MaxHealth;
        }
    }
}