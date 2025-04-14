using System;
using Player;
using UI;

namespace GameLogic
{
    public class GameOver
    {
        public event Action OnGameOver;

        private UISpawnManager _uiSpawnManager;

        public GameOver(UISpawnManager uiSpawnManager)
        {
            _uiSpawnManager = uiSpawnManager;
        }
        
        public void EndGame()
        {
            OnGameOver.Invoke();
            _uiSpawnManager.SpawnRestartPanel();
        }
    }
}