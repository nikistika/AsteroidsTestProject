using System;
using System.Collections;
using Characters;
using Factories;
using GameLogic;
using Player;
using Shooting;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        
        private bool _flagGameOver;
        private InputCharacter _inputCharacter;
        private ShootingMissile _shootingMissile;
        private ShootingLaser _shootingLaser;
        private DataSpaceShip _dataSpaceShip;
        private WaitForSeconds _waitRespawnAsteroidRange;
        private WaitForSeconds _waitRespawnUFORange;

        [SerializeField] private GameOver _gameOver;
        [SerializeField] private AsteroidFactory _asteroidFactory;
        [SerializeField] private MissileFactory _missileFactory;
        [SerializeField] private UFOFactory _ufoFactory;
        [SerializeField] private GameplayUI _gameplayUI;
        
        [SerializeField] private SpaceShip _spaceShip;
        
        [SerializeField] private float _respawnAsteroidRange = 3;
        [SerializeField] private float _minRespawnUFORange = 5;
        [SerializeField] private float _maxRespawnUFORange = 10;

        private void Awake()
        {
            SpawnSpaceShip();
            _waitRespawnAsteroidRange = new WaitForSeconds(_respawnAsteroidRange);
            _waitRespawnUFORange = new WaitForSeconds(Random.Range(_minRespawnUFORange, _maxRespawnUFORange));
            _gameOver.OnGameOver += GameOver;
        }

        private void Start()
        {
            StartCoroutine(SpawnAsteroidsCoroutine());
            StartCoroutine(SpawnUFOCoroutine());
        }

        private void SpawnSpaceShip()
        {
            var objectSpaceShip = Instantiate(_spaceShip);
            
            _inputCharacter = objectSpaceShip.GetComponent<InputCharacter>();
            _shootingMissile = objectSpaceShip.GetComponent<ShootingMissile>();
            _shootingLaser = objectSpaceShip.GetComponent<ShootingLaser>();
            _dataSpaceShip = objectSpaceShip.GetComponent<DataSpaceShip>();
            
            _inputCharacter.Construct(_gameOver);
            _shootingMissile.Construct(_missileFactory);
            _missileFactory.Construct(objectSpaceShip, _shootingMissile);
            _ufoFactory.Construct(objectSpaceShip, _dataSpaceShip);
            _asteroidFactory.Construct(_dataSpaceShip);
            _gameplayUI.Construct(_shootingLaser, _dataSpaceShip);
            _gameOver.Construct(_dataSpaceShip);
        }
        
        private Asteroid SpawnAsteroid()
        {
            var asteroid = _asteroidFactory.SpawnObject();
            asteroid.OnReturnAsteroid += ReturnAsteroid;
            asteroid.OnGetAsteroid += SpawnAsteroid;
            return asteroid;
        }

        private void SpawnUFO()
        {
            var ufo = _ufoFactory.SpawnObject();
            ufo.OnReturnUFO += ReturnUFO;
        }

        private void ReturnAsteroid(Asteroid asteroid)
        {
            asteroid.OnReturnAsteroid -= ReturnAsteroid;
            asteroid.OnGetAsteroid -= SpawnAsteroid;
            _asteroidFactory.ReturnObject(asteroid);
        }

        private void ReturnUFO(UFO ufo)
        {
            ufo.OnReturnUFO -= ReturnUFO;
            _ufoFactory.ReturnObject(ufo);
        }

        private IEnumerator SpawnAsteroidsCoroutine()
        {
            while (!_flagGameOver)
            {
                SpawnAsteroid();
                yield return _waitRespawnAsteroidRange;
            }
        }

        private IEnumerator SpawnUFOCoroutine()
        {
            while (!_flagGameOver)
            {
                SpawnUFO();
                yield return _waitRespawnUFORange;
            }
        }

        private void GameOver()
        {
            _flagGameOver = true;
        }
    }
}