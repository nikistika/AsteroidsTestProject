using GameLogic;
using Managers;
using Zenject;

namespace UI
{
    public class UIRestartSpawnManager
    {
        private RestartPanel _restartPanel;
        private ScoreManager _scoreManager;
        private IInstantiator _instantiator;
        private GameplayUI _gameplayUI;
        private GameOver _gameOver;
            
        public UIRestartSpawnManager(ScoreManager scoreManager, RestartPanel restartPanel,
             IInstantiator instantiator, GameplayUI gameplayUI, GameOver gameOver)
        {
            _scoreManager = scoreManager;
            _restartPanel = restartPanel;
            _instantiator = instantiator;
            _gameplayUI = gameplayUI;
            _gameOver = gameOver;
        }

        public void StartWork()
        {
            _gameOver.OnGameOver += SpawnRestartPanel;
        }

        private void SpawnRestartPanel()
        {
            var restartPanelObject = _instantiator.InstantiatePrefab(_restartPanel, _gameplayUI.transform);
            var restartPanel = restartPanelObject.GetComponent<RestartPanel>();
            restartPanelObject.SetActive(true);
            restartPanel.ActivateRestartPanel(_scoreManager.CurrentScore);
            _gameOver.OnGameOver -= SpawnRestartPanel;
        }
    }
}