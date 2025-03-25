using GameLogic;
using Shooting;
using UI;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Characters
{
    public class Asteroid : Enemy
    {
        private ObjectPool<Asteroid> _asteroidPool;
        private bool _flagParent = true;

        [SerializeField] private int _scoreKill = 5;

        public void Construct(ObjectPool<Asteroid> asteroidPool, DataSpaceShip dataSpaceShip, GameOver gameOver)
        {
            _asteroidPool = asteroidPool;
            _dataSpaceShip = dataSpaceShip;
            _gameOver = gameOver;
        }

        private void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
            RandomScale();
        }

        private void Update()
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

        public void IsObjectParent(bool isObjectParent)
        {
            _flagParent = isObjectParent;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
                if (_flagParent) Crushing();
                else if (!_flagParent) transform.localScale *= 2;

                _dataSpaceShip.AddScore(_scoreKill);
                _asteroidPool.Release(this);
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

        private void GoingAbroad()
        {
            if (gameObject.transform.position.y > _halfHeightCamera + 1 ||
                gameObject.transform.position.y < -_halfHeightCamera - 1 ||
                gameObject.transform.position.x > _halfWidthCamera + 1 ||
                gameObject.transform.position.x < -_halfWidthCamera - 1)
            {
                if (!_flagParent) transform.localScale *= 2;
                _asteroidPool.Release(this);
            }
        }

        private void Crushing()
        {
            for (int i = 1; i <= 4; i++)
            {
                var fragment = _asteroidPool.Get();
                fragment.IsObjectParent(false);
                fragment.transform.position = transform.position;
                fragment.transform.localScale = transform.localScale / 2;
                fragment.MoveFragment(i, fragment);
            }
        }
    }
}