using System;
using GameLogic;
using Player;
using Shooting;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFO : Character
    {
        public event Action<UFO> OnReturnUFO;

        private SpaceShip _spaceShip;
        private GameOver _gameOver;
        private bool _flagGameOver;

        [SerializeField] private int _speed = 1;

        [Inject]
        public void Construct(GameOver gameOver, SpaceShip spaseShip, ScreenSize screenSize)
        {
            base.Construct(screenSize);
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

        protected override void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Move()
        {
            Vector2 direction = (_spaceShip.transform.position - transform.position).normalized;
            Rigidbody.velocity = direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
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
            _gameOver.OnGameOver -= GameOver;
            Rigidbody.velocity = Vector2.zero;
        }
    }
}