using Factories;
using GameLogic;
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
        private ShootingLaser _shootingLaser;
        private DataSpaceShip _dataSpaceShip;
        private InputKeyboard inputKeyboard;
        private MissileFactory _missileFactory;
        private GameplayUI _gameplayUI;
        private SpaceShip _spaceShipPrefab;
        private Missile _missilePrefab;
        private PoolSizeSO _missilePoolSizeData;
        
        public SpaceShip SpaceShipObject {get; private set;}

        public SpaceShipSpawnManager(GameOver gameOver, Camera camera,
            float halfHeightCamera, float halfWidthCamera,Missile missilePrefab, 
            GameplayUI gameplayUI, SpaceShip spaceShipPrefab, PoolSizeSO missilePoolSizeData) :
            base(gameOver, camera, halfHeightCamera, halfWidthCamera)
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
            _shootingLaser = objectSpaceShip.GetComponent<ShootingLaser>();
            _dataSpaceShip = objectSpaceShip.GetComponent<DataSpaceShip>();
        }

        private void DependencyTransfer(SpaceShip objectSpaceShip)
        {
            objectSpaceShip.Construct(_halfHeightCamera, _halfWidthCamera);
            _inputCharacter.Construct(_gameOver);
            
            _missileFactory = new MissileFactory(_camera, _halfHeightCamera, _halfWidthCamera,
                _missilePrefab, objectSpaceShip, _shootingMissile, _missilePoolSizeData);
            _missileFactory.StartWork();
            
            _shootingMissile.Construct(_missileFactory);
            _gameplayUI.Construct(_shootingLaser, _dataSpaceShip, _gameOver);
        }
    }
}