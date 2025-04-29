using Managers;
using Player;
using Shooting;
using UI;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class EntryPoint : MonoBehaviour
    {
        private AsteroidSpawnManager _asteroidSpawnManager;
        private UFOSpawnManager _ufoSpawnManager;
        private SpaceShipSpawnManager _spaceShipSpawnManager;
        private SpaceShip _spaceShip;
        private ShootingLaser _shootingLaser;
        private DataSpaceShip _dataSpaceShip;
        private UISpawnManager _uiSpawnManager;

        [Inject]
        public void Construct(
            SpaceShipSpawnManager spaceShipSpawnManager,
            AsteroidSpawnManager asteroidSpawnManager, 
            UISpawnManager uiSpawnManager,
            UFOSpawnManager ufoSpawnManager)
        {
            _spaceShipSpawnManager = spaceShipSpawnManager;
            _asteroidSpawnManager = asteroidSpawnManager;
            _uiSpawnManager = uiSpawnManager;
            _ufoSpawnManager = ufoSpawnManager;
        }

        private void Start()
        {
            _spaceShipSpawnManager.StartWork();
            _uiSpawnManager.StartWork();
            _asteroidSpawnManager.StartWork();
            _ufoSpawnManager.StartWork();
        }
    }
}