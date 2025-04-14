using Characters;
using Factories;
using Managers;
using Player;
using SciptableObjects;
using Shooting;
using UI;
using UnityEngine;
using Coroutine;

namespace GameLogic
{
    public class EntryPoint : MonoBehaviour
    {
        private Camera _camera;
        private float _halfHeightCamera;
        private float _halfWidthCamera;
        private GameOver _gameOver;
        
        private AsteroidFactory _asteroidFactory;
        private UFOFactory _ufoFactory;
        private MissileFactory _missileFactory;
        
        private SpaceShip _spaceShip;
        
        
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private UISpawnManager _uiSpawnManager;
        [SerializeField] private AsteroidSpawnManager _asteroidSpawnManager;
        [SerializeField] private UFOSpawnManager _ufoSpawnManager;
        [SerializeField] private SpaceShipSpawnManager _spaceShipSpawnManager;
        private DataSpaceShip _dataSpaceShip;
        
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private Missile _missilePrefab;
        [SerializeField] private SpaceShip _spaceShipPrefab;
        [SerializeField] private GameplayUI _gameplayUI;

        [SerializeField] private EnemySpawnManagerSO _asteroidSpawnData;
        [SerializeField] private EnemySpawnManagerSO _ufoSpawnData;
        
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        
        private void Awake()
        {
            DependencyInitialization();
        }

        private void DependencyInitialization()
        {
            _gameOver = new GameOver(_uiSpawnManager);
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            _gameOver = new GameOver(_uiSpawnManager);
            
            
            _spaceShipSpawnManager = new SpaceShipSpawnManager(_gameOver, _camera, _halfHeightCamera,
                _halfWidthCamera, _scoreManager, _asteroidFactory, _ufoFactory, _missilePrefab,
                _gameplayUI, _spaceShipPrefab);
            _spaceShipSpawnManager.StartWork();
            _spaceShip = _spaceShipSpawnManager.SpaceShipObject;
            _dataSpaceShip = _spaceShip.GetComponent<DataSpaceShip>();
            
            _asteroidFactory = new AsteroidFactory(_scoreManager, _gameOver, _camera, 
                _halfHeightCamera, _halfWidthCamera, _asteroidPrefab);
            _asteroidFactory.StartWork();
            
            _ufoFactory = new UFOFactory(_scoreManager, _gameOver, _camera, 
                _halfHeightCamera, _halfWidthCamera, _ufoPrefab, _spaceShip);
            _ufoFactory.StartWork();

            _asteroidSpawnManager = new AsteroidSpawnManager(_gameOver, _camera, _halfHeightCamera,
                _halfWidthCamera, _asteroidFactory, _asteroidSpawnData, _coroutinePerformer);
            _asteroidSpawnManager.StartWork();

            _ufoSpawnManager = new UFOSpawnManager(_gameOver, _camera, _halfHeightCamera,
                _halfWidthCamera, _ufoFactory, _ufoSpawnData , _coroutinePerformer);
            _ufoSpawnManager.StartWork();
            
        }   
    }
}
