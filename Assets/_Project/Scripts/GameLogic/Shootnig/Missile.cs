using _Project.Scripts.Characters.Enemies;
using GameLogic;
using UnityEngine;

namespace Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Missile : MonoBehaviour
    {
        private ScreenSize _screenSize;
        private Rigidbody2D _rigidbody;
        private ShootingMissile _shootingMissile;

        [SerializeField] private float _speed = 3;

        public void Construct(
            ShootingMissile shootingMissile, 
            ScreenSize screenSize)
        {
            _shootingMissile = shootingMissile;
            _screenSize = screenSize;
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
            if (collision.TryGetComponent<Asteroid>(out _) || collision.TryGetComponent<UFO>(out _))
            {
                _shootingMissile.InvokeOnReturnMissileToPool(this);
            }
        }

        private void GoingAbroad()
        {
            if (gameObject.transform.position.y > _screenSize.HalfHeightCamera ||
                gameObject.transform.position.y < -_screenSize.HalfHeightCamera ||
                gameObject.transform.position.x > _screenSize.HalfWidthCamera ||
                gameObject.transform.position.x < -_screenSize.HalfWidthCamera)
            {
                _shootingMissile.InvokeOnReturnMissileToPool(this);
            }
        }
    }
}