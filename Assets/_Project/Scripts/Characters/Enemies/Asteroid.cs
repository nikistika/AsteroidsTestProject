using System;
using Cysharp.Threading.Tasks;
using GameLogic;
using Service;
using Player;
using Shooting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : Character
    {
        public event Action<int, Asteroid> OnGetFragments;
        public event Action<Asteroid> OnReturnAsteroid;

        private bool _flagParent = true;
        private GameState _gameState;
        private IKillService _killService;
        private IRandomService _randomService;

        [SerializeField] private int _speed = 1;

        public void Construct(
            GameState gameState,
            ScreenSize screenSize,
            IKillService killService,
            IRandomService randomService)
        {
            base.Construct(screenSize);
            _gameState = gameState;
            _killService = killService;
            _randomService = randomService;
        }

        private void FixedUpdate()
        {
            GoingAbroad();
        }

        public void Initialize()
        {
            _gameState.OnGameOver += GameState;
            _gameState.OnGameContinue += GameContinue;
            _gameState.OnGameExit += GameExit;
            RandomScale();
        }
        
        public void Move()
        {
            Rigidbody.velocity = _randomService.GetRandomDirection(transform.position.y, transform.position.x) * _speed;
        }

        public void IsObjectParent(bool isObjectParent)
        {
            _flagParent = isObjectParent;
        }

        public void MoveFragment(Asteroid fragmentAsteroid)
        {
            fragmentAsteroid.Rigidbody.velocity = _randomService.GetRandomFragmentDirection();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Missile>(out _) || collision.TryGetComponent<Laser>(out _))
            {
                if (_flagParent)
                {
                    Crushing();
                }
                else if (!_flagParent)
                {
                    transform.localScale *= 2;
                }

                _killService.AddAsteroid(1);

                OnReturnAsteroid?.Invoke(this);
            }

            if (collision.TryGetComponent<SpaceShip>(out _))
            {
                _gameState.EndGame();
            }
        }
        
        private void RandomScale()
        {
            transform.localScale = _randomService.GetRandomScale();
        }

        private void Crushing()
        {
            OnGetFragments?.Invoke(4, this);
        }

        private void GameState()
        {
            Rigidbody.velocity = Vector2.zero;
        }

        private UniTask GameContinue()
        {
            gameObject.SetActive(false);
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