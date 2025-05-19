using System;
using Cysharp.Threading.Tasks;
using GameLogic;
using Managers;
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
        private KillService _killService;
        private float _scaleNumber1 = 1;
        private float _scaleNumber1_5 = 1.5f;
        private float _scaleNumber2 = 2f;
        private float _scaleNumber2_5 = 2.5f;
        private Vector2 _direction;

        [SerializeField] private int _speed = 1;

        public void Construct(
            GameState gameState,
            ScreenSize screenSize,
            KillService killService)
        {
            base.Construct(screenSize);
            _gameState = gameState;
            _killService = killService;
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
            if (transform.position.y > ScreenSize.HalfHeightCamera)
                Rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f) * _speed;
            else if (transform.position.y < -ScreenSize.HalfHeightCamera)
                Rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f) * _speed;
            else if (transform.position.x > ScreenSize.HalfWidthCamera)
                Rigidbody.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f)) * _speed;
            else if (transform.position.x < -ScreenSize.HalfWidthCamera)
                Rigidbody.velocity = new Vector2(1.0f, Random.Range(0, 0.5f)) * _speed;
        }

        public void IsObjectParent(bool isObjectParent)
        {
            _flagParent = isObjectParent;
        }

        public void MoveFragment(Asteroid fragmentAsteroid)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            fragmentAsteroid.Rigidbody.velocity = direction;
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
            if (Random.value > 0.5f)
            {
                transform.localScale = new Vector3(Random.Range(_scaleNumber1, _scaleNumber1_5),
                    Random.Range(_scaleNumber1, _scaleNumber1_5), _scaleNumber2);
            }
            else
            {
                transform.localScale = new Vector3(Random.Range(_scaleNumber1_5, _scaleNumber2_5),
                    Random.Range(_scaleNumber1_5, _scaleNumber2_5), _scaleNumber2);
            }
        }

        private void Crushing()
        {
            OnGetFragments?.Invoke(4, this);
        }

        private void GameState()
        {
            _direction = Rigidbody.velocity;
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