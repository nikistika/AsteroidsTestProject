using Player;
using UnityEngine;

namespace UI
{
    public class UISpawnManager : MonoBehaviour
    {
        
        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private Canvas _uiCanvas;

        public void SpawnRestartPanel(DataSpaceShip dataSpaceShip)
        {
            var restartPanel = Instantiate(_restartPanel, _uiCanvas.transform); 
            restartPanel.gameObject.SetActive(true);
            restartPanel.ActivateRestartPanel(dataSpaceShip.CurrentScore);
        }
    }
}