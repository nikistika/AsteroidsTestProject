using Factories;
using Managers;
using Player;
using UnityEngine;

namespace GameLogic
{
    public class EntryPoint : MonoBehaviour
    {
        private Camera _camera;
        private float _halfHeightCamera;
        private float _halfWidthCamera;

        [SerializeField] private AsteroidSpawmManager _asteroidSpawmManager;
        [SerializeField] private SpaceShipSpawnManager _spaceShipSpawnManager;
        [SerializeField] private AsteroidFactory _asteroidFactory;
        [SerializeField] private DataSpaceShip _dataSpaceShip;

        private void Awake()
        {
            DependencyInitialization();
            DependencyTransfer();
        }

        private void DependencyInitialization()
        {
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
        }

        private void DependencyTransfer()
        {
            _asteroidSpawmManager.Construct(_camera, _halfHeightCamera, _halfWidthCamera);
            _spaceShipSpawnManager.Construct(_camera, _halfHeightCamera, _halfWidthCamera);
            _asteroidFactory.Construct(_dataSpaceShip, _camera, _halfHeightCamera, _halfWidthCamera);
        }
    }
}
