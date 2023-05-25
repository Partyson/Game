using System;

namespace Game.Model.EntityModel
{
    public class Player : Entity
    {
        public int BulletLimit { get; protected set; }
        public double MaxHealth { get; protected set; }
        public double  HeathRegeneration { get; protected set; }
        public double ReloadSpeed {get; protected set; }

        public Player(int x, int y, Action<Entity> onPlayerOnEntityDied, int health = 100, int damage = 20) : base(x, y, onPlayerOnEntityDied, health, damage)
        {
        }

        public void TakeBooster(Booster booster)
        {
            var offset = booster.BoosterData.Value;
            switch (booster.BoosterData.Type)
            {
                case BoosterType.Damage:
                    Damage += offset;
                    break;
                case BoosterType.BulletLimit:
                    BulletLimit += (int)offset;
                    break;
                case BoosterType.HeathRegeneration:
                    HeathRegeneration += offset;
                    break;
                case BoosterType.MaxHealth:
                    MaxHealth += offset;
                    break;
                case BoosterType.ReloadSpeed:
                default:
                    ReloadSpeed -= offset;
                    break;
            }
        }
    }
}