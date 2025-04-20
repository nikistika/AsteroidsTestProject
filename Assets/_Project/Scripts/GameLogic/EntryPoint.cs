using Characters;
using Factories;
using Managers;
using Player;
using SciptableObjects;
using Shooting;
using UI;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class EntryPoint : MonoBehaviour
    {
        private AsteroidFactory _asteroidFactory;
        private UFOFactory _ufoFactory;
        private AsteroidSpawnManager _asteroidSpawnManager;
        private UFOSpawnManager _ufoSpawnManager;
        private SpaceShipSpawnManager _spaceShipSpawnManager;
        private ScoreManager _scoreManager;
        private SpaceShip _spaceShip;
        private UFO _ufoPrefab;
        private GameOver _gameOver;
        private ScreenSize _screenSize;
        private PoolSizeSO _missilePoolSizeData;
        private EnemySpawnManagerSO _ufoSpawnData;
        private UIRestartSpawnManager _uiRestartSpawnManager;
        private ShootingLaser _shootingLaser;
        private DataSpaceShip _dataSpaceShip;

        [Inject]
        public void Construct(ScoreManager scoreManager, SpaceShipSpawnManager spaceShipSpawnManager,
            AsteroidFactory asteroidFactory, AsteroidSpawnManager asteroidSpawnManager,
            ScreenSize screenSize, UFO ufoPrefab, GameOver gameOver,
            [Inject(Id = "UFOPoolSizeData")] PoolSizeSO missilePoolSizeData,
            EnemySpawnManagerSO ufoSpawnData, UIRestartSpawnManager uiRestartSpawnManager)
        {
            _spaceShipSpawnManager = spaceShipSpawnManager;
            _asteroidFactory = asteroidFactory;
            _asteroidSpawnManager = asteroidSpawnManager;
            _ufoPrefab = ufoPrefab;
            _gameOver = gameOver;
            _screenSize = screenSize;
            _missilePoolSizeData = missilePoolSizeData;
            _ufoSpawnData = ufoSpawnData;
            _scoreManager = scoreManager;
            _uiRestartSpawnManager = uiRestartSpawnManager;
        }

        private void Start()
        {
            _scoreManager.StartWork();
            _spaceShipSpawnManager.StartWork();

            _spaceShip = _spaceShipSpawnManager.SpaceShipObject;
            _shootingLaser = _spaceShipSpawnManager.ShootingLaser;
            _dataSpaceShip = _spaceShipSpawnManager.DataSpaceShip;
            
            _ufoFactory = new UFOFactory(_scoreManager, _gameOver,
                _screenSize, _ufoPrefab, _spaceShip, _missilePoolSizeData);

            _ufoSpawnManager = new UFOSpawnManager(_gameOver, _screenSize,
                _ufoFactory, _ufoSpawnData, _scoreManager);
            
            // _uiSpawnManager = new UISpawnManager()
            
            _asteroidFactory.StartWork();
            _ufoFactory.StartWork();
            _asteroidSpawnManager.StartWork();
            _ufoSpawnManager.StartWork();
            // _uiSpawnManager.StartWork();
            
        }
    }
}