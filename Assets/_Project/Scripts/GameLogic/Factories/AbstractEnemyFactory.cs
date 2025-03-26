using GameLogic;
using Managers;
using Player;
using UnityEngine;

namespace Factories
{
    public abstract class AbstractEnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        private float _halfHeightCamera;
        private float _halfWidthCamera;
        private Camera _camera;

        [SerializeField] protected SpawnManager _spawnManager;
        [SerializeField] protected DataSpaceShip _dataSpaceShip;
        [SerializeField] protected GameOver _gameOver;

        protected void Awake()
        {
            base.Awake();
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
        }

        protected Vector2 GetRandomSpawnPosition()
        {
            var randomIndex = Random.Range(1, 5);

            switch (randomIndex)
            {
                case 1:
                    return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), _halfHeightCamera + 0.5f);
                case 2:
                    return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), -_halfHeightCamera - 0.5f);
                case 3:
                    return new Vector2(_halfWidthCamera + 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
                case 4:
                    return new Vector2(-_halfWidthCamera - 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
            }

            return new Vector2(0, 0);
        }
    }
}