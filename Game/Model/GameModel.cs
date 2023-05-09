using System;
using System.Collections.Generic;
using Game.Model.EntityModel;

namespace Game.Model
{
    public class GameModel
    {
        private GameState _gameState;
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                GameStateChanged?.Invoke(value);
            }
        } 
        
        public Player Player;
        public List<Enemy> Enemies;
        public List<Booster> Boosters;

        public event Action<GameState> GameStateChanged;
        
        public void StartGame()
        {
            Player = new Player(0, 0, GameOver);
        }

        public void SpawnEnemy()
        {
            //TODO: спавн нового врага
        }

        public void SpawnBooster()
        {
            //TODO: спавн нового бустера
        }
        
        public void GameOver() => GameState = GameState.GameOver;
    }
}