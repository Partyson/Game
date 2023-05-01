using System;
using System.Collections.Generic;
using System.Drawing;
using Game.Model;
using Game.Model.Entity;

namespace Game
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
        public Map Map;
        public List<Enemy> Enemies;
        public List<Booster> Boosters;

        public event Action<GameState> GameStateChanged;
        
        public void StartGame()
        {
            Player = new Player(256 / 64, 128 / 64);
            Player.PlayerDied += GameOver;

            Map = new Map();
            Map.GenerateMap();
            
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