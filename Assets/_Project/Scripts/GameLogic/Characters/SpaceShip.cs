using Shooting;
using UI;
using UnityEngine;

namespace Characters
{
    public class SpaceShip : Character
    {
        private ShootingMissile _shootingMissile;

        [SerializeField] private float _speedMove = 70f;
        [SerializeField] private float _speedRotate = 2f;
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private ShootingLaser _shootingLaser;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            _shootingMissile = gameObject.GetComponent<ShootingMissile>();
        }

        private void Update()
        {
            Move();
            GoingAbroad();
            ShowedDataInUI();
        }

        public override void Move()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _rigidbody.AddRelativeForce(Vector2.up * (_speedMove * Time.deltaTime), ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody.MoveRotation(_rigidbody.rotation - _speedRotate);
            }

            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody.MoveRotation(_rigidbody.rotation + _speedRotate);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                ShootingMissile();
            }

            if (Input.GetKey(KeyCode.G))
            {
                ShootingLaser();
            }
        }

        private void GoingAbroad()
        {
            if (gameObject.transform.position.y > _halfHeightCamera)
            {
                _rigidbody.MovePosition(new Vector2(gameObject.transform.position.x, -_halfHeightCamera));
            }

            if (gameObject.transform.position.y < -_halfHeightCamera)
            {
                _rigidbody.MovePosition(new Vector2(gameObject.transform.position.x, _halfHeightCamera));
            }

            if (gameObject.transform.position.x > _halfWidthCamera)
            {
                _rigidbody.MovePosition(new Vector2(-_halfWidthCamera, gameObject.transform.position.y));
            }

            if (gameObject.transform.position.x < -_halfWidthCamera)
            {
                _rigidbody.MovePosition(new Vector2(_halfWidthCamera, gameObject.transform.position.y));
            }
        }

        private void ShootingMissile()
        {
            _shootingMissile.ShotMissile();
        }

        private void ShootingLaser()
        {
            _shootingLaser.Shot();
        }

        private void ShowedDataInUI()
        {
            Vector2 coordinates = transform.position;
            string coordinatesText = coordinates.ToString();
            Vector2 rotation = transform.rotation.eulerAngles;
            string rotationText = rotation.ToString();
            Vector2 speed = _rigidbody.velocity;
            string speedText = speed.ToString();


            Debug.Log($"coordinates: {coordinatesText}, rotation: {rotationText}, speed: {speedText}");
            _gameplayUI.DisplayDataAboutCharacter(coordinatesText, rotationText, speedText);
        }
    }
}