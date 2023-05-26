using System;

namespace Game.Model.EntityModel
{
    public class Player : Entity
    { 
        private double MaxHealth { get; set; }
        private double HeathRegeneration { get; set; }

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
                    if (Health.Equals(MaxHealth))
                        Health *= offset;
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