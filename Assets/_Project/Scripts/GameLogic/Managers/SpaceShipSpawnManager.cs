using Factories;
using Player;
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

        [SerializeField] private AsteroidFactory _asteroidFactory;
        [SerializeField] private MissileFactory _missileFactory;
        [SerializeField] private UFOFactory _ufoFactory;
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private SpaceShip _spaceShip;

        private void Start()
        {
            SpawnObject();
        }

        protected override SpaceShip SpawnObject()
        {
            var objectSpaceShip = Instantiate(_spaceShip);

            GetComponentsSpaceShip(objectSpaceShip);
            DependencyTransfer(objectSpaceShip);
            return objectSpaceShip;
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
            _inputCharacter.Construct(_gameOver);
            _shootingMissile.Construct(_missileFactory);
            _missileFactory.Construct(objectSpaceShip, _shootingMissile);
            _ufoFactory.Construct(objectSpaceShip, _dataSpaceShip);
            _asteroidFactory.Construct(_dataSpaceShip);
            _gameplayUI.Construct(_shootingLaser, _dataSpaceShip);
            _gameOver.Construct(_dataSpaceShip);
        }
    }
}