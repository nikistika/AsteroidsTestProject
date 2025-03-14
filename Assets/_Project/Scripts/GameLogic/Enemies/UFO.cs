using System;
using GameLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class UFO : MonoBehaviour
{

    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private GameplayUI _gameplayUI;
    private RestartPanel _restartPanel;
    private ObjectPool<UFO> _ufoPool;

    [SerializeField] private int _scoreKill = 10;


    public void Construct(ObjectPool<UFO> ufoPool, GameplayUI gameplayUI, RestartPanel restartPanel)
    {
        _ufoPool = ufoPool;
        _gameplayUI = gameplayUI;
        _restartPanel = restartPanel;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _halfHeightCamera = _camera.orthographicSize;
        _halfWidthCamera = _halfHeightCamera * _camera.aspect;
    }

    public void Move()
    {
        if (transform.position.y > _halfHeightCamera) _rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
        else if (transform.position.y < -_halfHeightCamera) _rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
        else if (transform.position.x > _halfWidthCamera) _rigidbody2D.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
        else if (transform.position.x < -_halfWidthCamera) _rigidbody2D.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
        {

            _gameplayUI.AddScore(_scoreKill);
            _ufoPool.Release(this);
        }

        if (collision.GetComponent<SpaceShip>())
        {
            _restartPanel.ActivateRestartPanel(_gameplayUI.CurrentScore);
        }
    }

}
