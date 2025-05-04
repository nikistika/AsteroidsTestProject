using Factories;
using GameLogic;
using GameLogic.Analytics;
using InputSystem;
using Player;
using SciptableObjects;
using Shooting;
using UnityEngine;

namespace Managers
{
    public class SpaceShipSpawnManager : BaseSpawnManager<SpaceShip>
    {
        private InputCharacter _inputCharacter;
        private ShootingMissile _shootingMissile;
        private InputKeyboard _inputKeyboard;
        private MissileFactory _missileFactory;

        private readonly SpaceShip _spaceShipPrefab;
        private readonly Missile _missilePrefab;
        private readonly PoolSizeSO _missilePoolSizeData;
        private readonly ShipRepository _shipRepository;
        private readonly AnalyticsController _analyticsController;
        private readonly KillManager _killManager;

        public SpaceShip SpaceShipObject { get; private set; }
        public ShootingLaser ShootingLaser { get; private set; }
        public DataSpaceShip DataSpaceShip { get; private set; }

        public SpaceShipSpawnManager(
            GameOver gameOver,
            ScreenSize screenSize, 
            Missile missilePrefab,
            SpaceShip spaceShipPrefab, 
            PoolSizeSO missilePoolSizeData,
            ShipRepository shipRepository,
            AnalyticsController analyticsController,
            KillManager killManager) :
            base(gameOver, screenSize)
        {
            _missilePrefab = missilePrefab;
            _spaceShipPrefab = spaceShipPrefab;
            _missilePoolSizeData = missilePoolSizeData;
            _shipRepository = shipRepository;
            _analyticsController = analyticsController;
            _killManager = killManager;
        }

        public override SpaceShip SpawnObject()
        {
            var objectSpaceShip = Object.Instantiate(_spaceShipPrefab);
            objectSpaceShip.Construct(_analyticsController, _killManager);
            GetComponentsSpaceShip(objectSpaceShip);
            DependencyTransfer(objectSpaceShip);
            return objectSpaceShip;
        }

        protected override void Initialize()
        {
            SpaceShipObject = SpawnObject();
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
            objectSpaceShip.Construct(ScreenSize);
            _inputCharacter.Construct(GameOver);

            _missileFactory = new MissileFactory(ScreenSize,
                _missilePrefab, objectSpaceShip, _shootingMissile, _missilePoolSizeData);
            _missileFactory.StartWork();

            _shootingMissile.Construct(_missileFactory, _killManager);
        }
    }
}