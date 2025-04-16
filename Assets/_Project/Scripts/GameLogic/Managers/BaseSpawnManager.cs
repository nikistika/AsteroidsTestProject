using GameLogic;
using UnityEngine;

namespace Managers
{
    public abstract class BaseSpawnManager<T>
    {
        protected bool FlagGameOver;
        protected Camera Camera;
        protected float HalfHeightCamera;
        protected float HalfWidthCamera;
        protected GameOver GameOver;
        protected ScoreManager ScoreManager;

        protected BaseSpawnManager(GameOver gameOver, Camera camera, 
            float halfHeightCamera, float halfWidthCamera, ScoreManager scoreManager)
        {
            GameOver = gameOver;
            Camera = camera;
            HalfHeightCamera = halfHeightCamera;
            HalfWidthCamera = halfWidthCamera;
            ScoreManager = scoreManager;
        }
        
        public void StartWork()
        {
            BaseInitialize();
            Initialize();
        }

        public abstract T SpawnObject();

        private void BaseInitialize()
        {
            GameOver.OnGameOver += GameOverHandler;
        }
        
        protected abstract void Initialize();

        private void GameOverHandler()
        {
            GameOver.OnGameOver -= GameOverHandler;
            FlagGameOver = true;
        }
    }
}