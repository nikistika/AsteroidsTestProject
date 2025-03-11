using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManagerAsteroids : MonoBehaviour
{
    
    private ObjectPool<Asteroid> _asteroidPool;
    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private Camera _camera;
    private bool _gameOver;
    
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private float _respawnRange = 3;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private int _maxPoolSize = 20;
    
    private void Awake()
    {
        _camera = Camera.main;
        _halfHeightCamera = _camera.orthographicSize;
        _halfWidthCamera = _halfHeightCamera * _camera.aspect;
        PoolInitialization();
    }

    private void Start()
    {
        StartCoroutine(SpawnAsteroidsCoroutine());
    }


    private void SpawnAsteroid()
    {
        _asteroidPool.Get();
    }

    private Vector2 getRandomSpawnPosition()
    {
        var randomIndex = Random.Range(1, 5);

        switch (randomIndex)
        {
            
            case 1:
                return new Vector2(Random.Range(- _halfWidthCamera, _halfWidthCamera), _halfHeightCamera + 0.5f);            
            case 2:
                return new Vector2(Random.Range(- _halfWidthCamera, _halfWidthCamera), - _halfHeightCamera - 0.5f);            
            case 3:
                return new Vector2(_halfWidthCamera + 0.5f, Random.Range(- _halfHeightCamera, _halfHeightCamera));            
            case 4:
                return new Vector2(- _halfWidthCamera - 0.5f, Random.Range(- _halfHeightCamera, _halfHeightCamera));            
        }
        return new Vector2(0, 0);
    }
    
    private void PoolInitialization()
    {
        _asteroidPool = new ObjectPool<Asteroid>(
            createFunc: () =>
            {
                var asteroid = Instantiate(_asteroid);
                asteroid.Construct(_asteroidPool, gameplayUI);
                asteroid.gameObject.transform.position = getRandomSpawnPosition();
                return asteroid;
            },  // Функция создания объекта
            actionOnGet: (obj) =>
            {
                obj.gameObject.SetActive(true);
                obj.gameObject.transform.position = getRandomSpawnPosition();
                obj.Move();
            },   // Действие при выдаче объекта
            actionOnRelease: (obj) =>
            {
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                obj.gameObject.SetActive(false);
            }, // Действие при возврате в пул
            actionOnDestroy: (obj) => Destroy(obj),   // Действие при удалении объекта
            collectionCheck: false, // Проверять ли повторное добавление объекта в пул
            defaultCapacity: _poolSize, // Начальный размер пула
            maxSize: _maxPoolSize // Максимальный размер пула
        );
    }

    private IEnumerator SpawnAsteroidsCoroutine()
    {
        while (!_gameOver)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(_respawnRange);
        }
        
    }

}
