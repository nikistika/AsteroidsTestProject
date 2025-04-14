using Player;
using UnityEngine;

namespace UI
{
    public class UISpawnManager : MonoBehaviour
    {
        
        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private ScoreManager _scoreManager;

        public void SpawnRestartPanel()
        {
            var restartPanel = Instantiate(_restartPanel, _uiCanvas.transform); 
            restartPanel.gameObject.SetActive(true);
            restartPanel.ActivateRestartPanel(_scoreManager.CurrentScore);
        }
    }
}