using _Project.Scripts.Audio;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.EntryPoints;
using _Project.Scripts.Enums;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Factories;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.GameLogic.Services.Spawners;
using _Project.Scripts.GameLogic.Shootnig;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.UI.GameScene;
using UnityEngine;
using Zenject;
using Asteroid = _Project.Scripts.Characters.Enemies.Asteroid;

namespace _Project.Scripts.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        private Camera _camera;

        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private Missile _missilePrefab;
        [SerializeField] private SpaceShip _spaceShipPrefab;
        [SerializeField] private GameplayUIView _gameplayUIView;
        [SerializeField] private EnemySpawnManagerConfig _asteroidSpawnData;
        [SerializeField] private EnemySpawnManagerConfig _ufoSpawnData;
        [SerializeField] private PoolSizeConfig _asteroidPoolSizeData;
        [SerializeField] private PoolSizeConfig _ufoPoolSizeData;
        [SerializeField] private PoolSizeConfig _missilePoolSizeData;

        public override void InstallBindings()
        {
            _camera = Camera.main;
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();

            Container.Bind<ShipRepository>().AsSingle();

            Container.Bind<EnemySpawnManagerConfig>().WithId(GameInstallerIDs.AsteroidSizeData)
                .FromInstance(_asteroidSpawnData).AsCached();
            Container.Bind<EnemySpawnManagerConfig>().WithId(GameInstallerIDs.UFOSizeData).FromInstance(_ufoSpawnData)
                .AsCached();

            Container.Bind<Asteroid>().FromInstance(_asteroidPrefab).AsSingle();
            Container.Bind<UFO>().FromInstance(_ufoPrefab).AsSingle();
            Container.Bind<Missile>().FromInstance(_missilePrefab).AsSingle();
            Container.Bind<SpaceShip>().FromInstance(_spaceShipPrefab).AsSingle();
            Container.Bind<GameplayUIView>().FromInstance(_gameplayUIView).AsSingle();

            Container.Bind<PoolSizeConfig>().WithId(GameInstallerIDs.AsteroidPoolSizeData)
                .FromInstance(_asteroidPoolSizeData).AsCached();
            Container.Bind<PoolSizeConfig>().WithId(GameInstallerIDs.UFOPoolSizeData)
                .FromInstance(_ufoPoolSizeData).AsCached();
            Container.Bind<PoolSizeConfig>().WithId(GameInstallerIDs.MissilePoolSizeData)
                .FromInstance(_missilePoolSizeData).AsCached();

            Container.Bind<ScreenSize>().AsSingle();
            Container.BindInterfacesTo<RandomService>().AsSingle();
            Container.BindInterfacesTo<ScoreService>().AsSingle();
            Container.Bind<GameState>().AsSingle();
            Container.BindInterfacesTo<KillService>().AsSingle();
            Container.Bind<GameplayUIFactory>().AsSingle();
            Container.Bind<SpaceShipSpawner>().AsSingle();
            Container.Bind<AsteroidFactory>().AsSingle();
            Container.Bind<UFOFactory>().AsSingle();
            Container.Bind<UfoSpawner>().AsSingle();
            Container.Bind<AsteroidSpawner>().AsSingle();
            Container.Bind<MissileFactory>().AsSingle();
            
            Container.Bind<AudioControllerSpawner>().AsSingle();
            Container.BindInterfacesTo<AudioService>().AsSingle();

            Container.BindInterfacesTo<GameEntryPoint>().AsSingle();
        }
    }
}