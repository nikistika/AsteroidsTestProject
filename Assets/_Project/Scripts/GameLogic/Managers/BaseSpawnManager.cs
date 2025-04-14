using GameLogic;
using UnityEngine;

namespace Managers
{
    public abstract class BaseSpawnManager<T>
    {
        protected bool _flagGameOver;
        protected Camera _camera;
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        protected GameOver _gameOver;

        public BaseSpawnManager(GameOver gameOver, Camera camera, float halfHeightCamera, float halfWidthCamera)
        {
            _gameOver = gameOver;
            _camera = camera;
            _halfHeightCamera = halfHeightCamera;
            _halfWidthCamera = halfWidthCamera;
        }
        
        public void StartWork()
        {
            BaseInitialize();
            Initialize();
        }

        public abstract T SpawnObject();

        private void BaseInitialize()
        {
            _gameOver.OnGameOver += GameOver;
        }
        
        protected abstract void Initialize();

        private void GameOver()
        {
            _gameOver.OnGameOver -= GameOver;
            _flagGameOver = true;
        }
    }
}