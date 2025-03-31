using System;
using Player;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class GameOver : MonoBehaviour
    {
        public event Action OnGameOver;

        [SerializeField] private RestartPanel _restartPanel;
        private DataSpaceShip _dataSpaceShip;

        public void Construct(DataSpaceShip dataSpaceShip)
        {
            _dataSpaceShip = dataSpaceShip;
        }
        
        public void EndGame()
        {
            OnGameOver.Invoke();
            _restartPanel.gameObject.SetActive(true);
            _restartPanel.ActivateRestartPanel(_dataSpaceShip.CurrentScore);
        }
    }
}