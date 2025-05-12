using System;
using GameLogic;
using Managers;
using Player;
using Shooting;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : Character
    {
        public event Action<int, Asteroid> OnGetFragments;
        public event Action<Asteroid> OnReturnAsteroid;

        private bool _flagParent = true;
        private GameOver _gameOver;
        private KillService _killService;
        private float _scaleNumber1 = 1;
        private float _scaleNumber1_5 = 1.5f;
        private float _scaleNumber2 = 2f;
        private float _scaleNumber2_5 = 2.5f;

        [SerializeField] private int _speed = 1;

        public void Construct(
            GameOver gameOver,
            ScreenSize screenSize,
            KillService killService)
        {
            BaseInitialize(screenSize);
            _gameOver = gameOver;
            _killService = killService;
        }

        private void Start()
        {
            _gameOver.OnGameOver += GameOver;
        }

        private void FixedUpdate()
        {
            GoingAbroad();
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

        protected void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            RandomScale();
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
                _gameOver.EndGame();
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

        private void GameOver()
        {
            _gameOver.OnGameOver -= GameOver;
            Rigidbody.velocity = Vector2.zero;
        }
    }
}