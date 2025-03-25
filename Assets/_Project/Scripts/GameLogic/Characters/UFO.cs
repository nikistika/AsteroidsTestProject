using GameLogic;
using Shooting;
using UI;
using UnityEngine;
using UnityEngine.Pool;

namespace Characters
{
    public class UFO : Enemy
    {
        private ObjectPool<UFO> _ufoPool;
        private SpaceShip _spaceShip;

        [SerializeField] private int _scoreKill = 10;
        [SerializeField] private int _speed = 1;


        public void Construct(ObjectPool<UFO> ufoPool, DataSpaceShip dataSpaceShip, GameOver gameOver,
            SpaceShip spaseShip)
        {
            base.Awake();

            _ufoPool = ufoPool;
            _dataSpaceShip = dataSpaceShip;
            _gameOver = gameOver;
            _spaceShip = spaseShip;
            
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            Vector2 direction = (_spaceShip.transform.position - transform.position).normalized;
            _rigidbody.velocity = direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
                _dataSpaceShip.AddScore(_scoreKill);
                _ufoPool.Release(this);
            }

            if (collision.GetComponent<SpaceShip>())
            {
                _gameOver.EndGame();
            }
        }
    }
}