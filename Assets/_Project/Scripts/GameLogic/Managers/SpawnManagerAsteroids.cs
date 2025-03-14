using System.Collections;
using Characters;
using UI;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    public class SpawnManagerAsteroids : MonoBehaviour
    {
        private ObjectPool<Asteroid> _asteroidPool;
        private ObjectPool<UFO> _ufoPool;
        private float _halfHeightCamera;
        private float _halfWidthCamera;
        private Camera _camera;
        private bool _gameOver;

        [SerializeField] private RestartPanel _respawnPanel;
        [SerializeField] private GameplayUI _gameplayUI;
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

        private Vector2 getRandomSpawnPosition()
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
                    asteroid.Construct(_asteroidPool, _gameplayUI, _respawnPanel);
                    asteroid.gameObject.transform.position = getRandomSpawnPosition();
                    return asteroid;
                }, // Функция создания объекта
                actionOnGet: (obj) =>
                {
                    obj.gameObject.SetActive(true);
                    obj.gameObject.transform.position = getRandomSpawnPosition();
                    obj.Move();
                    obj.IsObjectParent(true);
                }, // Действие при выдаче объекта
                actionOnRelease: (obj) =>
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                }, // Действие при возврате в пул
                actionOnDestroy: (obj) => Destroy(obj), // Действие при удалении объекта
                collectionCheck: false, // Проверять ли повторное добавление объекта в пул
                defaultCapacity: _poolSizeAsteroids, // Начальный размер пула
                maxSize: _maxPoolSizeAsteroids // Максимальный размер пула
            );
        }

        private void UFOPoolInitialization()
        {
            _ufoPool = new ObjectPool<UFO>(
                createFunc: () =>
                {
                    var UFO = Instantiate(_ufo);
                    UFO.Construct(_ufoPool, _gameplayUI, _respawnPanel, _spaseShip);
                    UFO.gameObject.transform.position = getRandomSpawnPosition();
                    return UFO;
                }, // Функция создания объекта
                actionOnGet: (obj) =>
                {
                    obj.gameObject.SetActive(true);
                    obj.gameObject.transform.position = getRandomSpawnPosition();
                    obj.Move();
                }, // Действие при выдаче объекта
                actionOnRelease: (obj) =>
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                }, // Действие при возврате в пул
                actionOnDestroy: (obj) => Destroy(obj), // Действие при удалении объекта
                collectionCheck: false, // Проверять ли повторное добавление объекта в пул
                defaultCapacity: _poolSizeUFO, // Начальный размер пула
                maxSize: _maxPoolSizeUFO // Максимальный размер пула
            );
        }

        private IEnumerator SpawnAsteroidsCoroutine()
        {
            while (!_gameOver)
            {
                SpawnAsteroid();
                yield return new WaitForSeconds(_respawnAsteroidRange);
            }
        }

        private IEnumerator SpawnUFOCoroutine()
        {
            while (!_gameOver)
            {
                SpawnUFO();
                yield return new WaitForSeconds(Random.Range(_minRespawnUFORange, _maxRespawnUFORange));
            }
        }
    }
}