using GameLogic;
using UnityEngine;

namespace Managers
{
    public abstract class BaseSpawnManager<T> : MonoBehaviour
    {
        protected bool _flagGameOver;

        [SerializeField] protected GameOver _gameOver;

        private void Awake()
        {
            _gameOver.OnGameOver += GameOver;
        }

        protected abstract T SpawnObject();

        private void GameOver()
        {
            _gameOver.OnGameOver -= GameOver;
            _flagGameOver = true;
        }
    }
}