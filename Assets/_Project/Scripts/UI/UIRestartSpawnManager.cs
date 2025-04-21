using Managers;
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
        
        public void SpawnRestartPanel()
        {
            var restartPanelObject = _iInstantiator.InstantiatePrefab(_restartPanel, _gameplayUI.transform);
            var restartPanel = restartPanelObject.GetComponent<RestartPanel>();
            restartPanelObject.SetActive(true);
            restartPanel.ActivateRestartPanel(_scoreManager.CurrentScore);
        }
    }
}