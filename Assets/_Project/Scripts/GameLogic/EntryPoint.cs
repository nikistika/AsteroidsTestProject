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
        private AsteroidSpawnManager _asteroidSpawnManager;
        private UFOSpawnManager _ufoSpawnManager;
        private SpaceShipSpawnManager _spaceShipSpawnManager;
        private ScoreManager _scoreManager;
        private UISpawnManager _uiSpawnManager;
        
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private Missile _missilePrefab;
        [SerializeField] private SpaceShip _spaceShipPrefab;
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private EnemySpawnManagerSO _asteroidSpawnData;
        [SerializeField] private EnemySpawnManagerSO _ufoSpawnData;
        [SerializeField] private PoolSizeSO _asteroidPoolSizeData;
        [SerializeField] private PoolSizeSO _ufoPoolSizeData;
        [SerializeField] private PoolSizeSO _missilePoolSizeData;
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private Canvas _uiCanvas;
        
        private void Awake()
        {
            DependencyInitialize();
        }

        private void DependencyInitialize()
        {
            _scoreManager = new ScoreManager();
            _scoreManager.StartWork();
            
            _uiSpawnManager = new UISpawnManager();
            _uiSpawnManager.Construct(_scoreManager, _restartPanel, _uiCanvas);
            
            _gameOver = new GameOver(_uiSpawnManager);
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            _gameOver = new GameOver(_uiSpawnManager);
            
            _spaceShipSpawnManager = new SpaceShipSpawnManager(_gameOver, _camera, _halfHeightCamera,
                _halfWidthCamera, _missilePrefab, _gameplayUI, _spaceShipPrefab, _missilePoolSizeData, _scoreManager);
            _spaceShipSpawnManager.StartWork();
            _spaceShip = _spaceShipSpawnManager.SpaceShipObject;
            
            _asteroidFactory = new AsteroidFactory(_scoreManager, _gameOver, 
                _halfHeightCamera, _halfWidthCamera, _asteroidPrefab, _asteroidPoolSizeData);
            _asteroidFactory.StartWork();
            
            _ufoFactory = new UFOFactory(_scoreManager, _gameOver, _camera, 
                _halfHeightCamera, _halfWidthCamera, _ufoPrefab, _spaceShip, _missilePoolSizeData);
            _ufoFactory.StartWork();

            _asteroidSpawnManager = new AsteroidSpawnManager(_gameOver, _camera, _halfHeightCamera,
                _halfWidthCamera, _asteroidFactory, _asteroidSpawnData, _coroutinePerformer, _scoreManager);
            _asteroidSpawnManager.StartWork();

            _ufoSpawnManager = new UFOSpawnManager(_gameOver, _camera, _halfHeightCamera,
                _halfWidthCamera, _ufoFactory, _ufoSpawnData , _coroutinePerformer, _scoreManager);
            _ufoSpawnManager.StartWork();
        }   
    }
}
