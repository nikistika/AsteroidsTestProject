using System;
using GameLogic;
using Managers;
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

        private ShipRepository _shipRepository;
        private GameOver _gameOver;
        private KillManager _killManager;
        private bool _flagGameOver;

        [SerializeField] private int _speed = 1;

        [Inject]
        public void Construct(
            GameOver gameOver,
            ShipRepository shipRepository,
            ScreenSize screenSize,
            KillManager killManager)
        {
            base.Construct(screenSize);
            _gameOver = gameOver;
            _shipRepository = shipRepository;
            _killManager = killManager;
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
            Vector2 direction = (_shipRepository.SpaceShip.transform.position - transform.position).normalized;
            Rigidbody.velocity = direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Missile>(out _) || collision.TryGetComponent<Laser>(out _))
            {
                _killManager.AddUFO(1);

                OnReturnUFO?.Invoke(this);
            }

            if (collision.TryGetComponent<SpaceShip>(out _))
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