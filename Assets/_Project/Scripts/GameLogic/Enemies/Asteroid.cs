using System;
using GameLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    
    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private GameplayUI _gameplayUI;
    private RestartPanel _restartPanel;
    private ObjectPool<Asteroid> _asteroidPool;
    private bool _flagPerent = true;

    [SerializeField] private int _scoreKill = 5;
    
    public void Construct(ObjectPool<Asteroid> asteroidPool, GameplayUI gameplayUI, RestartPanel restartPanel)
    {
        _asteroidPool = asteroidPool;
        _gameplayUI = gameplayUI;
        _restartPanel = restartPanel;
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _halfHeightCamera = _camera.orthographicSize;
        _halfWidthCamera = _halfHeightCamera * _camera.aspect;

        RandomScale();
    }

    private void Update()
    {
        GoingAbroad();
    }

    public void Move()
    {
        if(transform.position.y > _halfHeightCamera) _rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
        else if(transform.position.y < - _halfHeightCamera) _rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
        else if(transform.position.x > _halfWidthCamera) _rigidbody2D.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
        else if(transform.position.x < - _halfWidthCamera) _rigidbody2D.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
    }

    public void MoveFragment(int fragmentNumber, Asteroid fragmentAsteroid)
    {
        if (fragmentNumber == 1) fragmentAsteroid._rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
        else if (fragmentNumber == 2) fragmentAsteroid._rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
        else if (fragmentNumber == 3) fragmentAsteroid._rigidbody2D.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
        else if (fragmentNumber == 4) fragmentAsteroid._rigidbody2D.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
    }

    public void IsObjectParent (bool isObjectParent)
    {
        _flagPerent = isObjectParent;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
        {

            if (_flagPerent) Crushing();
            _gameplayUI.AddScore(_scoreKill);
            _asteroidPool.Release(this);
        }

        if (collision.GetComponent<SpaceShip>())
        {
            _restartPanel.ActivateRestartPanel(_gameplayUI.CurrentScore);
        }
    }
    
    private void RandomScale()
    {
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 1:
                transform.localScale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1);
                break;
            case 2:
                transform.localScale = new Vector3(Random.Range(1f, 2f), Random.Range(1f, 2f), 1);
                break;
        }
    }
    
    private void GoingAbroad()
    {
        if (gameObject.transform.position.y > _halfHeightCamera +1 ||
            gameObject.transform.position.y < -_halfHeightCamera -1 ||
            gameObject.transform.position.x > _halfWidthCamera +1 ||
            gameObject.transform.position.x < -_halfWidthCamera -1)
        {
            _asteroidPool.Release(this);
        }
    }

    private void Crushing()
    {
        for (int i = 1; i <= 4; i++)
        {
            var fragment = _asteroidPool.Get();
            fragment._flagPerent = false;
            fragment.transform.position = transform.position;
            fragment.transform.localScale = transform.localScale / 2;
            fragment.MoveFragment(i, fragment);

        }
    }


}