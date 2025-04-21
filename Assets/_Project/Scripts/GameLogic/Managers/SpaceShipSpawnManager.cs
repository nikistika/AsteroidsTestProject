using Factories;
using GameLogic;
using InputSystem;
using Player;
using SciptableObjects;
using Shooting;
using UI;
using UnityEngine;

namespace Managers
{
    public class SpaceShipSpawnManager : BaseSpawnManager<SpaceShip>
    {
        private InputCharacter _inputCharacter;
        private ShootingMissile _shootingMissile;
        private InputKeyboard _inputKeyboard;
        private MissileFactory _missileFactory;
        private GameplayUI _gameplayUI;
        private SpaceShip _spaceShipPrefab;
        private Missile _missilePrefab;
        private PoolSizeSO _missilePoolSizeData;

        public SpaceShip SpaceShipObject { get; private set; }
        public ShootingLaser ShootingLaser { get; private set; }
        public DataSpaceShip DataSpaceShip { get; private set; }

        public SpaceShipSpawnManager(GameOver gameOver,
            ScreenSize screenSize, Missile missilePrefab,
            GameplayUI gameplayUI, SpaceShip spaceShipPrefab, PoolSizeSO missilePoolSizeData,
            ScoreManager scoreManager) :
            base(gameOver, screenSize, scoreManager)
        {
            _missilePrefab = missilePrefab;
            _gameplayUI = gameplayUI;
            _spaceShipPrefab = spaceShipPrefab;
            _missilePoolSizeData = missilePoolSizeData;
        }

        public override SpaceShip SpawnObject()
        {
            var objectSpaceShip = Object.Instantiate(_spaceShipPrefab);
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
        }

        private void DependencyTransfer(SpaceShip objectSpaceShip)
        {
            objectSpaceShip.Construct(ScreenSize);
            _inputCharacter.Construct(GameOver);

            _missileFactory = new MissileFactory(ScreenSize,
                _missilePrefab, objectSpaceShip, _shootingMissile, _missilePoolSizeData);
            _missileFactory.StartWork();

            _shootingMissile.Construct(_missileFactory);
        }
    }
}