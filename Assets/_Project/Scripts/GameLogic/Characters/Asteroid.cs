using System;
using GameLogic;
using Player;
using Shooting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    public class Asteroid : Enemy
    {
        
        public event Action<int, Asteroid> OnGetFragments;
        public event Action<Asteroid> OnReturnAsteroid;
        
        private bool _flagParent = true;

        [SerializeField] private int _scoreKill = 5;
        
        public void Construct(DataSpaceShip dataSpaceShip, GameOver gameOver)
        {
            _dataSpaceShip = dataSpaceShip;
            _gameOver = gameOver;
        }

        private void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
            RandomScale();
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
            if (transform.position.y > _halfHeightCamera)
                _rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
            else if (transform.position.y < -_halfHeightCamera)
                _rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
            else if (transform.position.x > _halfWidthCamera)
                _rigidbody.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
            else if (transform.position.x < -_halfWidthCamera)
                _rigidbody.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
        }

        public void IsObjectParent(bool isObjectParent)
        {
            _flagParent = isObjectParent;
        }

        public void MoveFragment(int fragmentNumber, Asteroid fragmentAsteroid)
        {
            if (fragmentNumber == 1) fragmentAsteroid._rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
            else if (fragmentNumber == 2)
                fragmentAsteroid._rigidbody.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
            else if (fragmentNumber == 3)
                fragmentAsteroid._rigidbody.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
            else if (fragmentNumber == 4)
                fragmentAsteroid._rigidbody.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
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
                
                _dataSpaceShip.AddScore(_scoreKill);
                
                if (OnReturnAsteroid != null)
                {
                    OnReturnAsteroid.Invoke(this);
                }
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
            _rigidbody.velocity = Vector2.zero;
        }
    }
}