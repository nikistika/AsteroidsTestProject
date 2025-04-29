using System;
using GameLogic;
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

        [SerializeField] private int _speed = 1;

        [Inject]
        public void Construct(
            GameOver gameOver, 
            ScreenSize screenSize)
        {
            base.Construct(screenSize);
            _gameOver = gameOver;
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

        public void MoveFragment(int fragmentNumber, Asteroid fragmentAsteroid)
        {
            if (fragmentNumber == 1)
                fragmentAsteroid.Rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
            else if (fragmentNumber == 2)
                fragmentAsteroid.Rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
            else if (fragmentNumber == 3)
                fragmentAsteroid.Rigidbody.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
            else if (fragmentNumber == 4)
                fragmentAsteroid.Rigidbody.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
        }

        protected override void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            RandomScale();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
                if (_flagParent)
                {
                    Crushing();
                }
                else if (!_flagParent)
                {
                    transform.localScale *= 2;
                }

                OnReturnAsteroid?.Invoke(this);
            }

            if (collision.GetComponent<SpaceShip>())
            {
                _gameOver.EndGame();
            }
        }

        private void RandomScale()
        {
            int randomIndex = Random.Range(0, 3);

            switch (randomIndex)
            {
                case 1:
                    transform.localScale = new Vector3(Random.Range(1f, 1.5f), Random.Range(1f, 1.5f), 2);
                    break;
                case 2:
                    transform.localScale = new Vector3(Random.Range(1.5f, 2.5f), Random.Range(1.5f, 2.5f), 2);
                    break;
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