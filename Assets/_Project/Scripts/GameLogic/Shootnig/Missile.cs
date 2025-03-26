using Characters;
using UnityEngine;

namespace Shooting
{
    public class Missile : MonoBehaviour
    {
        private float _halfHeightCamera;
        private float _halfWidthCamera;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private ShootingMissile _shootingMissile;

        [SerializeField] private float _speed = 3;

        public void Construct(ShootingMissile shootingMissile)
        {
            _shootingMissile = shootingMissile;
        }

        private void Awake()
        {
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Move();
        }

        private void FixedUpdate()
        {
            GoingAbroad();
        }

        public void Move()
        {
            _rigidbody.velocity = transform.parent.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Asteroid>() || collision.GetComponent<UFO>())
            {
                _shootingMissile.OnReturnMissileToPool.Invoke(this);
            }
        }

        private void GoingAbroad()
        {
            if (gameObject.transform.position.y > _halfHeightCamera ||
                gameObject.transform.position.y < -_halfHeightCamera ||
                gameObject.transform.position.x > _halfWidthCamera ||
                gameObject.transform.position.x < -_halfWidthCamera)
            {
                _shootingMissile.OnReturnMissileToPool.Invoke(this);
            }
        }
    }
}