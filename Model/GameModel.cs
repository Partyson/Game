using System;
using System.Collections.Generic;
using Game.Model.EntityModel;

namespace Game.Model
{
    public class GameModel
    {
        private GameState _gameState;
        private object lockObject = new object();
        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                GameStateChanged?.Invoke(value);
            }
        } 
        
        public Player Player { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<Booster> Boosters { get; private set; }
        public int Score { get; private set; }

        public event Action<GameState> GameStateChanged;
        
        public void StartGame()
        {
            Player = new Player(0, 0, GameOver);
            Score = 0;
            Enemies = new List<Enemy>();
            Boosters = new List<Booster>();
        }

        public void SpawnEnemy(int x, int y)
        {
            const int startHealth = 100;
            const int startDamage = 20;
            Enemies.Add(new Enemy(x, y, EnemyDied, startHealth, startDamage));
        }

        public void SpawnBooster()
        {
            //TODO: спавн нового бустера
        }
        
        public void GameOver()
        {
            GameState = GameState.GameOver;
        }

        private void EnemyDied() => Score++;
    }
}