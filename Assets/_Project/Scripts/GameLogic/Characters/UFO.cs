using GameLogic;
using Managers;
using Player;
using Shooting;
using UnityEngine;

namespace Characters
{
    public class UFO : Enemy
    {
        private SpaceShip _spaceShip;
        private bool _flagGameOver;

        [SerializeField] private int _scoreKill = 10;
        [SerializeField] private int _speed = 1;
        
        public void Construct(SpawnManager spawnManager, DataSpaceShip dataSpaceShip, GameOver gameOver,
            SpaceShip spaseShip)
        {
            base.Awake();

            _spawnManager = spawnManager;
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
                _spawnManager.OnReturnUFO.Invoke(this);
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