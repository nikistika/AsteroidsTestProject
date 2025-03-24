using UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace GameLogic
{
    public class GameOver : MonoBehaviour
    {
        
        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private DataSpaceShip _data;
        
        public void EndGame()
        {
            gameObject.SetActive(true);
            _restartPanel.ActivateRestartPanel(_data.CurrentScore);
        }
        
    }
}