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
        private GameplayUI _gameplayUIObject;
        private UIRestartSpawnManager _uiRestartSpawnManager;

        [Inject]
        public void Construct(SpaceShipSpawnManager spaceShipSpawnManager,
            AsteroidSpawnManager asteroidSpawnManager, UISpawnManager uiSpawnManager,
            UFOSpawnManager ufoSpawnManager, UIRestartSpawnManager uiRestartSpawnManager)
        {
            _spaceShipSpawnManager = spaceShipSpawnManager;
            _asteroidSpawnManager = asteroidSpawnManager;
            _uiSpawnManager = uiSpawnManager;
            _ufoSpawnManager = ufoSpawnManager;
            _uiRestartSpawnManager = uiRestartSpawnManager;
        }

        private void Start()
        {
            _spaceShipSpawnManager.StartWork();
            _uiSpawnManager.StartWork();
            _asteroidSpawnManager.StartWork();
            _ufoSpawnManager.StartWork();
            _uiRestartSpawnManager.StartWork();
        }
    }
}