using System;
using Cysharp.Threading.Tasks;
using GameLogic;
using Managers;
using Player;
using Shooting;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFO : Character
    {
        public event Action<UFO> OnReturnUFO;

        private ShipRepository _shipRepository;
        private GameState _gameState;
        private KillService _killService;
        private bool _flagGameOver;

        [SerializeField] private int _speed = 1;

        public void Construct(
            GameState gameState,
            ShipRepository shipRepository,
            ScreenSize screenSize,
            KillService killService)
        {
            base.Construct(screenSize);
            _gameState = gameState;
            _shipRepository = shipRepository;
            _killService = killService;
        }

        private void Start()
        {
            _gameState.OnGameOver += GameState;
            _gameState.OnGameContinue += GameContinue;
            _gameState.OnGameExit += GameExit;
        }

        private void FixedUpdate()
        {
            if (!_flagGameOver)
            {
                Move();
            }
        }

        public void Initialize()
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
                _killService.AddUFO(1);

                OnReturnUFO?.Invoke(this);
            }

            if (collision.TryGetComponent<SpaceShip>(out _))
            {
                _gameState.EndGame();
            }
        }

        private void GameState()
        {
            _flagGameOver = true;
            Rigidbody.velocity = Vector2.zero;
        }

        private UniTask GameContinue()
        {
            gameObject.SetActive(false);
            _flagGameOver = false;
            return UniTask.CompletedTask;
        }

        private void GameExit()
        {
            _gameState.OnGameOver -= GameState;
            _gameState.OnGameContinue -= GameContinue;
            _gameState.OnGameExit -= GameExit;
        }
    }
}