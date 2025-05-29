using ConfigData;
using Cysharp.Threading.Tasks;
using Factories;
using GameLogic;
using GameLogic.Analytics;
using GameLogic.Enums;
using InputSystem;
using LoadingAssets;
using Player;
using ScriptableObjects;
using Shooting;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class SpaceShipSpawner : BaseSpawner<SpaceShip>
    {
        private InputCharacter _inputCharacter;
        private ShootingMissile _shootingMissile;
        private InputKeyboard _inputKeyboard;
        private MissileFactory _missileFactory;
        private SpaceShip _spaceShipPrefab;

        private ShootingLaser _shootingLaser;
        private DataSpaceShip _dataSpaceShip;
        
        private readonly ShipRepository _shipRepository;
        private readonly AnalyticsController _analyticsController;
        private readonly KillService _killService;
        private readonly IAssetLoader _assetLoader;
        private readonly RemoteConfigService _remoteConfigService;

        public SpaceShip SpaceShipObject { get; private set; }

        public SpaceShipSpawner(
            GameState gameState,
            ScreenSize screenSize,

            ShipRepository shipRepository,
            AnalyticsController analyticsController,
            KillService killService,
            IAssetLoader assetLoader,
            RemoteConfigService remoteConfigService) :
            base(gameState, screenSize)
        {
            _shipRepository = shipRepository;
            _analyticsController = analyticsController;
            _killService = killService;
            _assetLoader = assetLoader;
            _remoteConfigService = remoteConfigService;
        }

        protected override async UniTask Initialize()
        {
            await GetPrefab();
            SpaceShipObject = await SpawnObject();
        }

        private async UniTask<SpaceShip> SpawnObject()
        {
            var objectSpaceShip = Object.Instantiate(_spaceShipPrefab);
            GetComponentsSpaceShip(objectSpaceShip);
            await DependencyTransfer(objectSpaceShip);
            return objectSpaceShip;
        }

        protected override UniTask GameContinue()
        {
            FlagGameOver = false;
            return UniTask.CompletedTask;
        }

        private async UniTask GetPrefab()
        {
            _spaceShipPrefab = await _assetLoader.CreateSpaceShip();
        }

        private void GetComponentsSpaceShip(SpaceShip objectSpaceShip)
        {
            _inputCharacter = objectSpaceShip.GetComponent<InputCharacter>();
            _shootingMissile = objectSpaceShip.GetComponent<ShootingMissile>();
            _shootingLaser = objectSpaceShip.GetComponent<ShootingLaser>();
            _dataSpaceShip = objectSpaceShip.GetComponent<DataSpaceShip>();

            _shipRepository.GetSpaceShip(objectSpaceShip, _shootingLaser, _dataSpaceShip);
        }

        private async UniTask DependencyTransfer(SpaceShip objectSpaceShip)
        {
            objectSpaceShip.Construct(_analyticsController, _killService, ScreenSize);
            objectSpaceShip.StartWork();
            _inputCharacter.Construct(GameState);

            _missileFactory = new MissileFactory(ScreenSize, objectSpaceShip, _shootingMissile,
                _assetLoader, _remoteConfigService);
            await _missileFactory.StartWork();

            _shootingMissile.Construct(_missileFactory, _killService);

            _shootingMissile.Initialize();
        }
    }
}