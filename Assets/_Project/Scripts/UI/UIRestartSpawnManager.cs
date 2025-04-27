using GameLogic;
using Managers;
using Zenject;

namespace UI
{
    public class UIRestartSpawnManager
    {
        private readonly RestartPanel _restartPanel;
        private readonly ScoreManager _scoreManager;
        private readonly IInstantiator _instantiator;
        private readonly GameplayUIRepository _gameplayUIRepository;
        private readonly GameOver _gameOver;
            
        public UIRestartSpawnManager(ScoreManager scoreManager, RestartPanel restartPanel,
             IInstantiator instantiator, GameplayUIRepository gameplayUIRepository, GameOver gameOver)
        {
            _scoreManager = scoreManager;
            _restartPanel = restartPanel;
            _instantiator = instantiator;
            _gameplayUIRepository = gameplayUIRepository;
            _gameOver = gameOver;
        }

        public void StartWork()
        {
            _gameOver.OnGameOver += SpawnRestartPanel;
        }

        private void SpawnRestartPanel()
        {
            var restartPanelObject = _instantiator.InstantiatePrefab(_restartPanel, 
                _gameplayUIRepository.GameplayUIObject.transform);
            var restartPanel = restartPanelObject.GetComponent<RestartPanel>();
            restartPanelObject.SetActive(true);
            restartPanel.ActivateRestartPanel(_scoreManager.CurrentScore);
            _gameOver.OnGameOver -= SpawnRestartPanel;
        }
    }
}