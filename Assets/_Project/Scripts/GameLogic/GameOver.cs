using System;
using UI;

namespace GameLogic
{
    public class GameOver
    {
        public event Action OnGameOver;

        private UIRestartSpawnManager _uiRestartSpawnManager;

        public GameOver(UIRestartSpawnManager uiRestartSpawnManager)
        {
            _uiRestartSpawnManager = uiRestartSpawnManager;
        }

        public void EndGame()
        {
            OnGameOver.Invoke();
            _uiRestartSpawnManager.SpawnRestartPanel();
        }
    }
}