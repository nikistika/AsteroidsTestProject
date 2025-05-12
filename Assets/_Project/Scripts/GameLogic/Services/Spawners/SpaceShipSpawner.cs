using Cysharp.Threading.Tasks;
using Factories;
using GameLogic;
using GameLogic.Analytics;
using GameLogic.Enums;
using InputSystem;
using LoadingAssets;
using Player;
using SciptableObjects;
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

        private readonly Missile _missilePrefab;
        private readonly PoolSizeSO _missilePoolSizeData;
        private readonly ShipRepository _shipRepository;
        private readonly AnalyticsController _analyticsController;
        private readonly KillService _killService;
        private readonly IAssetLoader _assetLoader;

        public SpaceShip SpaceShipObject { get; private set; }
        public ShootingLaser ShootingLaser { get; private set; }
        public DataSpaceShip DataSpaceShip { get; private set; }

        public SpaceShipSpawner(
            GameOver gameOver,
            ScreenSize screenSize, 
            Missile missilePrefab,
            [Inject(Id = GameInstallerIDs.MissilePoolSizeData)] PoolSizeSO missilePoolSizeData,
            ShipRepository shipRepository,
            AnalyticsController analyticsController,
            KillService killService,
            IAssetLoader assetLoader) :
            base(gameOver, screenSize)
        {
            _missilePrefab = missilePrefab;
            _missilePoolSizeData = missilePoolSizeData;
            _shipRepository = shipRepository;
            _analyticsController = analyticsController;
            _killService = killService;
            _assetLoader = assetLoader;
        }

        public async UniTask<SpaceShip> SpawnObject()
        {
            var objectSpaceShip = await _assetLoader.CreateSpaceShip();
            GetComponentsSpaceShip(objectSpaceShip);
            DependencyTransfer(objectSpaceShip);
            return objectSpaceShip;
        }

        protected override async UniTask Initialize()
        {
            SpaceShipObject = await SpawnObject();
        }

        private void GetComponentsSpaceShip(SpaceShip objectSpaceShip)
        {
            _inputCharacter = objectSpaceShip.GetComponent<InputCharacter>();
            _shootingMissile = objectSpaceShip.GetComponent<ShootingMissile>();
            ShootingLaser = objectSpaceShip.GetComponent<ShootingLaser>();
            DataSpaceShip = objectSpaceShip.GetComponent<DataSpaceShip>();

            _shipRepository.GetSpaceShip(objectSpaceShip, ShootingLaser, DataSpaceShip);
        }

        private void DependencyTransfer(SpaceShip objectSpaceShip)
        {
            objectSpaceShip.Initialize(_analyticsController, _killService, ScreenSize);
            _inputCharacter.Initialize(GameOver);
            
            _missileFactory = new MissileFactory(ScreenSize,
                _missilePrefab, objectSpaceShip, _shootingMissile, _missilePoolSizeData, _assetLoader);
            _missileFactory.StartWork();

            _shootingMissile.Construct(_missileFactory, _killService);
            
            _shootingMissile.Initialize();
        }
    }
}