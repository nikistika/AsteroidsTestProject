using System;
using Player;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class GameOver : MonoBehaviour
    {
        public event Action OnGameOver;

        [SerializeField] private UISpawnManager _uiSpawnManager;
        private DataSpaceShip _dataSpaceShip;

        public void Construct(DataSpaceShip dataSpaceShip)
        {
            _dataSpaceShip = dataSpaceShip;
        }
        
        public void EndGame()
        {
            OnGameOver.Invoke();

            _uiSpawnManager.SpawnRestartPanel(_dataSpaceShip);
        }
    }
}