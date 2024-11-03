using System;
using GameLogic;
using Player;
using Shooting;
using UnityEngine;

namespace Characters
{
    public class UFO : Character
    {
        public event Action<UFO> OnReturnUFO;
        
        private SpaceShip _spaceShip;
        private bool _flagGameOver;
        private DataSpaceShip _dataSpaceShip;
        private GameOver _gameOver;
        
        [SerializeField] private int _scoreKill = 10;
        [SerializeField] private int _speed = 1;
        
        public void Construct( DataSpaceShip dataSpaceShip, GameOver gameOver,
            SpaceShip spaseShip)
        {
            _dataSpaceShip = dataSpaceShip;
            _gameOver = gameOver;
            _spaceShip = spaseShip;
        }

        private void Start()
        {
            _gameOver.OnGameOver += GameOver;
        }

        private void FixedUpdate()
        {
            if (!_flagGameOver)
            {
                Move();
            }
        }

        protected override void Initialization()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
        }

        private void Move()
        {
            Vector2 direction = (_spaceShip.transform.position - transform.position).normalized;
            _rigidbody.velocity = direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
                _dataSpaceShip.AddScore(_scoreKill);
                OnReturnUFO?.Invoke(this);
            }

            if (collision.GetComponent<SpaceShip>())
            {
                _gameOver.EndGame();
            }
        }

        private void GameOver()
        {
            _flagGameOver = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}