using GameLogic;
using Managers;
using Player;
using Shooting;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIRestartSpawnManager
    {
        private RestartPanel _restartPanel;
        private ScoreManager _scoreManager;
        private IInstantiator _iInstantiator;
        private GameplayUI _gameplayUI;

            
        public UIRestartSpawnManager(ScoreManager scoreManager, RestartPanel restartPanel,
             IInstantiator iInstantiator, GameplayUI gameplayUI)
        {
            _scoreManager = scoreManager;
            _restartPanel = restartPanel;
            _iInstantiator = iInstantiator;
            _gameplayUI = gameplayUI;

        }

        // public void StartWork()
        // {
        //     SpawnUI();
        // }

        // private void SpawnUI()
        // {
        //     var gameplayUIObject = _iInstantiator.InstantiatePrefab(_gameplayUI);
        //     var gameplayUI = gameplayUIObject.GetComponent<GameplayUI>();
        //     gameplayUI.Construct(_shootingLaser, _dataSpaceShip, _gameOver, _scoreManager);
        // }
        
        public void SpawnRestartPanel()
        {
            var restartPanelObject = _iInstantiator.InstantiatePrefab(_restartPanel, _gameplayUI.transform);
            var restartPanel = restartPanelObject.GetComponent<RestartPanel>();
            restartPanelObject.SetActive(true);
            restartPanel.ActivateRestartPanel(_scoreManager.CurrentScore);
        }
    }
}