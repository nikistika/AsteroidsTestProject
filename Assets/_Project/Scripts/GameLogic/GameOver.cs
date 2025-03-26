using System;
using Player;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class GameOver : MonoBehaviour
    {
        public Action OnGameOver;

        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private DataSpaceShip _data;

        public void EndGame()
        {
            OnGameOver.Invoke();
            _restartPanel.gameObject.SetActive(true);
            _restartPanel.ActivateRestartPanel(_data.CurrentScore);
        }
    }
}