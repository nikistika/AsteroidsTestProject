using Managers;
using UnityEngine;

namespace UI
{
    public class UISpawnManager
    {
        private RestartPanel _restartPanel;
        private Canvas _uiCanvas;
        private ScoreManager _scoreManager;

        public void Construct(ScoreManager scoreManager, RestartPanel restartPanel, Canvas uiCanvas)
        {
            _scoreManager = scoreManager;
            _restartPanel = restartPanel;
            _uiCanvas = uiCanvas;
        }
        
        public void SpawnRestartPanel()
        {
            var restartPanel = Object.Instantiate(_restartPanel, _uiCanvas.transform); 
            restartPanel.gameObject.SetActive(true);
            restartPanel.ActivateRestartPanel(_scoreManager.CurrentScore);
        }
    }
}