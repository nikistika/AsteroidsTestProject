using Characters;
using Factories;
using GameLogic;
using GameLogic.Enums;
using Managers;
using Player;
using SciptableObjects;
using Shooting;
using UI;
using UI.View;
using UnityEngine;
using Zenject;
using Asteroid = Characters.Asteroid;

namespace Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        private Camera _camera;

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

        public override void InstallBindings()
        {
            _camera = Camera.main;
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();

            Container.Bind<ShipRepository>().AsSingle();

            Container.Bind<EnemySpawnManagerSO>().WithId(GameInstallerIDs.AsteroidSizeData)
                .FromInstance(_asteroidSpawnData).AsCached();
            Container.Bind<EnemySpawnManagerSO>().WithId(GameInstallerIDs.UFOSizeData).FromInstance(_ufoSpawnData).AsCached();

            Container.Bind<Asteroid>().FromInstance(_asteroidPrefab).AsSingle();
            Container.Bind<UFO>().FromInstance(_ufoPrefab).AsSingle();
            Container.Bind<Missile>().FromInstance(_missilePrefab).AsSingle();
            Container.Bind<SpaceShip>().FromInstance(_spaceShipPrefab).AsSingle();
            Container.Bind<GameplayUIView>().FromInstance(_gameplayUIView).AsSingle();

            Container.Bind<PoolSizeSO>().WithId(GameInstallerIDs.AsteroidPoolSizeData)
                .FromInstance(_asteroidPoolSizeData).AsCached();
            Container.Bind<PoolSizeSO>().WithId(GameInstallerIDs.UFOPoolSizeData)
                .FromInstance(_ufoPoolSizeData).AsCached();
            Container.Bind<PoolSizeSO>().WithId(GameInstallerIDs.MissilePoolSizeData)
                .FromInstance(_missilePoolSizeData).AsCached();

            Container.Bind<ScreenSize>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
            Container.Bind<GameOver>().AsSingle();
            Container.BindInterfacesAndSelfTo<KillManager>().AsSingle();
            Container.Bind<UISpawner>().AsSingle();
            Container.Bind<SpaceShipSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UFOFactory>().AsSingle();
            Container.Bind<UfoSpawner>().AsSingle();
            Container.Bind<AsteroidSpawner>().AsSingle();
            Container.Bind<MissileFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle();
        }
    }
}