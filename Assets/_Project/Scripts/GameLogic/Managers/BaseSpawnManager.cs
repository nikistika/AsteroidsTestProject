using GameLogic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public abstract class BaseSpawnManager<T> : MonoBehaviour
    {
        protected bool _flagGameOver;
        protected Camera _camera;
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        
        [SerializeField] protected GameOver _gameOver;
        
        public void Construct(Camera camera, float halfHeightCamera, float halfWidthCamera)
        {
            _camera = camera;
            _halfHeightCamera = halfHeightCamera;
            _halfWidthCamera = halfWidthCamera;
        }
        
        private void Awake()
        {
            _gameOver.OnGameOver += GameOver;
            Initialization();
        }

        protected abstract T SpawnObject();
        
        protected abstract void Initialization();

        private void GameOver()
        {
            _gameOver.OnGameOver -= GameOver;
            _flagGameOver = true;
        }
    }
}