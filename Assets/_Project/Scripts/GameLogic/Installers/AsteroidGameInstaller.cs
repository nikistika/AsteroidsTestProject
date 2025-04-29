using Characters;
using Factories;
using Managers;
using Player;
using SciptableObjects;
using Shooting;
using UI;
using UI.View;
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
        private ScreenSize _screenSize;
        private ShipRepository _shipRepository;
        
        private UISpawnManager _uiSpawnManager;
        private UFOFactory _ufoFactory;
        private UFOSpawnManager _ufoSpawnManager;
        private GameplayUIRepository _gameplayUIRepository;
        
        [Inject] private IInstantiator _instantiator;

        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private Missile _missilePrefab;
        [SerializeField] private SpaceShip _spaceShipPrefab;
        [SerializeField] private GameplayUIView _gameplayUIView;
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
            
            _shipRepository = new ShipRepository();
            Container.Bind<ShipRepository>().FromInstance(_shipRepository).AsSingle();
            
            _gameplayUIRepository = new GameplayUIRepository();
            Container.Bind<GameplayUIRepository>().FromInstance(_gameplayUIRepository).AsSingle();
            
            Container.Bind<EnemySpawnManagerSO>().FromInstance(_ufoSpawnData).AsSingle();

            Container.Bind<RestartPanel>().FromInstance(_restartPanel).AsSingle();

            Container.Bind<UFO>().FromInstance(_ufoPrefab).AsSingle();
            Container.Bind<Missile>().FromInstance(_missilePrefab).AsSingle();

            _screenSize = new ScreenSize(_camera);
            Container.Bind<ScreenSize>().FromInstance(_screenSize).AsSingle();

            _scoreManager = new ScoreManager();
            Container.Bind<ScoreManager>().FromInstance(_scoreManager).AsSingle();

            _gameOver = new GameOver();
            Container.Bind<GameOver>().FromInstance(_gameOver).AsSingle();
            
            _uiSpawnManager = new UISpawnManager(_scoreManager, _instantiator, 
            _shipRepository, _gameOver, _gameplayUIRepository, _gameplayUIView);
            Container.Bind<UISpawnManager>().FromInstance(_uiSpawnManager).AsSingle();
            
            _spaceShipSpawnManager = new SpaceShipSpawnManager(_gameOver, _screenSize,
                _missilePrefab, _spaceShipPrefab, _missilePoolSizeData, _shipRepository);
            Container.Bind<SpaceShipSpawnManager>().FromInstance(_spaceShipSpawnManager).AsSingle();

            _asteroidFactory = new AsteroidFactory(_scoreManager, _gameOver,
                _screenSize, _asteroidPrefab, _asteroidPoolSizeData);
            Container.Bind<AsteroidFactory>().FromInstance(_asteroidFactory).AsSingle();
            
            _ufoFactory = new UFOFactory(_scoreManager, _gameOver,
                _screenSize, _ufoPrefab, _shipRepository, _missilePoolSizeData);
            Container.Bind<UFOFactory>().FromInstance(_ufoFactory).AsSingle();
            
            _ufoSpawnManager = new UFOSpawnManager(_gameOver, _screenSize,
                _ufoFactory, _ufoSpawnData);
            Container.Bind<UFOSpawnManager>().FromInstance(_ufoSpawnManager).AsSingle();
            
            _asteroidSpawnManager = new AsteroidSpawnManager(_gameOver,
                _screenSize, _asteroidFactory, _asteroidSpawnData);
            Container.Bind<AsteroidSpawnManager>().FromInstance(_asteroidSpawnManager).AsSingle();

            Container.Bind<MissileFactory>().FromInstance(_missileFactory).AsSingle();

            Container.Bind<PoolSizeSO>().WithId("UFOPoolSizeData")
                .FromInstance(_ufoPoolSizeData).AsSingle();
            
            Container.Bind<EntryPoint>().FromInstance(_entryPoint).AsSingle();
            
            Container.Bind<IInitializable>().FromInstance(_scoreManager).AsCached();
            Container.Bind<IInitializable>().FromInstance(_asteroidFactory).AsCached();
            Container.Bind<IInitializable>().FromInstance(_ufoFactory).AsCached();
            
            Container.Inject(_entryPoint);
        }
    }
}