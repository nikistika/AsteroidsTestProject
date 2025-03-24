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
            
            _ufoPool = ufoPool;
            _dataSpaceShip = dataSpaceShip;
            _gameOver = gameOver;
            _spaceShip = spaseShip;
            
            Debug.Log($"_spaceShip: {_spaceShip}");
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            Vector3 direction = (_spaceShip.transform.position - transform.position).normalized;
            Debug.Log($"direction: {direction}");

            transform.Translate(direction * (_speed * Time.deltaTime));
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