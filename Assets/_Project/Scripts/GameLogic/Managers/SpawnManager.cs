using System;
using System.Collections;
using Characters;
using Factories;
using GameLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        
        private bool _flagGameOver;
        private WaitForSeconds _waitRespawnAsteroidRange;
        private WaitForSeconds _waitRespawnUFORange;

        [SerializeField] private GameOver _gameOver;
        [SerializeField] private AsteroidFactory _asteroidFactory;
        [SerializeField] private UFOFactory _ufoFactory;

        [SerializeField] private float _respawnAsteroidRange = 3;
        [SerializeField] private float _minRespawnUFORange = 5;
        [SerializeField] private float _maxRespawnUFORange = 10;

        private void Awake()
        {
            _waitRespawnAsteroidRange = new WaitForSeconds(_respawnAsteroidRange);
            _waitRespawnUFORange = new WaitForSeconds(Random.Range(_minRespawnUFORange, _maxRespawnUFORange));

            _gameOver.OnGameOver += GameOver;
        }

        private void Start()
        {
            StartCoroutine(SpawnAsteroidsCoroutine());
            StartCoroutine(SpawnUFOCoroutine());
        }
        
        private Asteroid SpawnAsteroid()
        {
            Debug.Log("SpawnManager: Spawning Asteroid");
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

        public void ReturnAsteroid(Asteroid asteroid)
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