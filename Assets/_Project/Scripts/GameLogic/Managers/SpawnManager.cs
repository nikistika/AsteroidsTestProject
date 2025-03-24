using System.Collections;
using Characters;
using GameLogic;
using UI;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private ObjectPool<Asteroid> _asteroidPool;
        private ObjectPool<UFO> _ufoPool;
        private float _halfHeightCamera;
        private float _halfWidthCamera;
        private Camera _camera;
        private bool _flagGameOver;
        private WaitForSeconds _waitRespawnAsteroidRange;
        private WaitForSeconds _waitRespawnUFORange;

        [SerializeField] private GameOver _gameOver;
        [SerializeField] private DataSpaceShip _dataSpaceShip;
        [SerializeField] private Asteroid _asteroid;
        [SerializeField] private UFO _ufo;
        [SerializeField] private float _respawnAsteroidRange = 3;
        [SerializeField] private float _minRespawnUFORange = 5;
        [SerializeField] private float _maxRespawnUFORange = 10;
        [SerializeField] private int _poolSizeAsteroids = 20;
        [SerializeField] private int _maxPoolSizeAsteroids = 50;
        [SerializeField] private int _poolSizeUFO = 5;
        [SerializeField] private int _maxPoolSizeUFO = 10;
        [SerializeField] private SpaceShip _spaseShip;

        private void Awake()
        {
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            AsteroidPoolInitialization();
            UFOPoolInitialization();
            
            _waitRespawnAsteroidRange = new WaitForSeconds(_respawnAsteroidRange);
            _waitRespawnUFORange = new WaitForSeconds(Random.Range(_minRespawnUFORange, _maxRespawnUFORange));
        }

        private void Start()
        {
            StartCoroutine(SpawnAsteroidsCoroutine());
            StartCoroutine(SpawnUFOCoroutine());
        }


        private void SpawnAsteroid()
        {
            _asteroidPool.Get();
        }

        private void SpawnUFO()
        {
            _ufoPool.Get();
        }

        private Vector2 GetRandomSpawnPosition()
        {
            var randomIndex = Random.Range(1, 5);

            switch (randomIndex)
            {
                case 1:
                    return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), _halfHeightCamera + 0.5f);
                case 2:
                    return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), -_halfHeightCamera - 0.5f);
                case 3:
                    return new Vector2(_halfWidthCamera + 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
                case 4:
                    return new Vector2(-_halfWidthCamera - 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
            }

            return new Vector2(0, 0);
        }

        private void AsteroidPoolInitialization()
        {
            _asteroidPool = new ObjectPool<Asteroid>(
                createFunc: () =>
                {
                    var asteroid = Instantiate(_asteroid);
                    asteroid.Construct(_asteroidPool, _dataSpaceShip, _gameOver);
                    asteroid.gameObject.transform.position = GetRandomSpawnPosition();
                    return asteroid;
                },
                actionOnGet: (obj) =>
                {
                    obj.gameObject.SetActive(true);
                    obj.gameObject.transform.position = GetRandomSpawnPosition();
                    obj.Move();
                    obj.IsObjectParent(true);
                },
                actionOnRelease: (obj) =>
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                },
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: _poolSizeAsteroids,
                maxSize: _maxPoolSizeAsteroids
            );
        }

        private void UFOPoolInitialization()
        {
            _ufoPool = new ObjectPool<UFO>(
                createFunc: () =>
                {
                    var UFO = Instantiate(_ufo);
                    UFO.Construct(_ufoPool, _dataSpaceShip, _gameOver, _spaseShip);
                    UFO.gameObject.transform.position = GetRandomSpawnPosition();
                    return UFO;
                },
                actionOnGet: (obj) =>
                {
                    obj.gameObject.SetActive(true);
                    obj.gameObject.transform.position = GetRandomSpawnPosition();
                    obj.Move();
                },
                actionOnRelease: (obj) =>
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                },
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: _poolSizeUFO,
                maxSize: _maxPoolSizeUFO
            );
        }

        private IEnumerator SpawnAsteroidsCoroutine()
        {
            while (!_gameOver)
            {
                
                SpawnAsteroid();
                yield return _waitRespawnAsteroidRange;
            }
        }

        private IEnumerator SpawnUFOCoroutine()
        {
            while (!_gameOver)
            {
                SpawnUFO();
                yield return _waitRespawnUFORange;
            }
        }
    }
}