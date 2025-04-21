using Characters;
using Factories;
using Managers;
using Player;
using SciptableObjects;
using Shooting;
using UI;
using UnityEngine;
using Zenject;

namespace GameLogic.Installers
{
    public sealed class AsteroidGameInstaller : MonoInstaller
    {
        private Camera _camera;
        private GameOver _gameOver;
        private AsteroidFactory _asteroidFactory;
        private MissileFactory _missileFactory;
        private AsteroidSpawnManager _asteroidSpawnManager;
        private SpaceShipSpawnManager _spaceShipSpawnManager;
        private ScoreManager _scoreManager;
        private UIRestartSpawnManager _uiRestartSpawnManager;
        private ScreenSize _screenSize;
        [Inject] private IInstantiator _iInstantiator;

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
        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private EntryPoint _entryPoint;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            _camera = Camera.main;

            Container.Bind<EnemySpawnManagerSO>().FromInstance(_ufoSpawnData).AsSingle();

            Container.Bind<RestartPanel>().FromInstance(_restartPanel).AsSingle();

            Container.Bind<UFO>().FromInstance(_ufoPrefab).AsSingle();
            Container.Bind<Missile>().FromInstance(_missilePrefab).AsSingle();

            _screenSize = new ScreenSize(_camera);
            Container.Bind<ScreenSize>().FromInstance(_screenSize).AsSingle();

            _scoreManager = new ScoreManager();
            Container.Bind<ScoreManager>().FromInstance(_scoreManager).AsSingle();

            _uiRestartSpawnManager = new UIRestartSpawnManager(_scoreManager, _restartPanel, _iInstantiator, _gameplayUI);
            Container.Bind<UIRestartSpawnManager>().FromInstance(_uiRestartSpawnManager).AsSingle();

            _gameOver = new GameOver(_uiRestartSpawnManager);
            Container.Bind<GameOver>().FromInstance(_gameOver).AsSingle();

            _spaceShipSpawnManager = new SpaceShipSpawnManager(_gameOver, _screenSize,
                _missilePrefab, _gameplayUI, _spaceShipPrefab, _missilePoolSizeData, _scoreManager);
            Container.Bind<SpaceShipSpawnManager>().FromInstance(_spaceShipSpawnManager).AsSingle();

            _asteroidFactory = new AsteroidFactory(_scoreManager, _gameOver,
                _screenSize, _asteroidPrefab, _asteroidPoolSizeData);
            Container.Bind<AsteroidFactory>().FromInstance(_asteroidFactory).AsSingle();

            _asteroidSpawnManager = new AsteroidSpawnManager(_gameOver,
                _screenSize, _asteroidFactory, _asteroidSpawnData, _scoreManager);
            Container.Bind<AsteroidSpawnManager>().FromInstance(_asteroidSpawnManager).AsSingle();

            Container.Bind<MissileFactory>().FromInstance(_missileFactory).AsSingle();

            Container.Bind<PoolSizeSO>().WithId("UFOPoolSizeData")
                .FromInstance(_ufoPoolSizeData).AsSingle();
            
            Container.Bind<GameplayUI>().FromInstance(_gameplayUI).AsSingle();

            Container.Bind<EntryPoint>().FromInstance(_entryPoint).AsSingle();
            Container.Inject(_entryPoint);
            
            

        }
    }
}