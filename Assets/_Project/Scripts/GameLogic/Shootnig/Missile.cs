using Characters;
using UnityEngine;

namespace Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Missile : MonoBehaviour
    {
        private float _halfHeightCamera;
        private float _halfWidthCamera;
        private Rigidbody2D _rigidbody;
        private ShootingMissile _shootingMissile;

        [SerializeField] private float _speed = 3;

        public void Construct(ShootingMissile shootingMissile, float halfHeightCamera, float halfWidthCamera)
        {
            _shootingMissile = shootingMissile;
            _halfHeightCamera = halfHeightCamera;
            _halfWidthCamera = halfWidthCamera;
        }

        private void Awake()
        {
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
                _shootingMissile.InvokeOnReturnMissileToPool(this);
            }
        }

        private void GoingAbroad()
        {
            if (gameObject.transform.position.y > _halfHeightCamera ||
                gameObject.transform.position.y < -_halfHeightCamera ||
                gameObject.transform.position.x > _halfWidthCamera ||
                gameObject.transform.position.x < -_halfWidthCamera)
            {
                _shootingMissile.InvokeOnReturnMissileToPool(this);
            }
        }
    }
}