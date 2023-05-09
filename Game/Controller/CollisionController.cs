using System.Collections.Generic;
using System.Linq;
using Game.Model;
using Game.Model.EntityModel;

namespace Game.Controller
{
    public class CollisionController
    {
        private readonly GameModel _gameModel;

        public CollisionController(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public bool TryGetPlayerCollision(out Entity intersectedEntity)
        {
            var entities = new List<Entity>(_gameModel.Boosters.Concat<Entity>(_gameModel.Enemies));
            foreach (var entity in entities.Where(entity => _gameModel.Player.HitBox.IntersectsWith(entity.HitBox)))
            {
                intersectedEntity = entity;
                return true;
            }

            intersectedEntity = null;
            return false;
        }
    }
}