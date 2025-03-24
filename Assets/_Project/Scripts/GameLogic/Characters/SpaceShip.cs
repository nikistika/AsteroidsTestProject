using Shooting;
using UI;
using UnityEngine;

namespace Characters
{
    public class SpaceShip : Character
    {
        private ShootingMissile _shootingMissile;

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
            GoingAbroad();
            // ShowedDataInUI();
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

        public void ShootingMissile()
        {
            _shootingMissile.ShotMissile();
        }

        public void ShootingLaser()
        {
            _shootingLaser.Shot();
        }

        // private void ShowedDataInUI()
        // {
        //     Vector2 coordinates = transform.position;
        //     string coordinatesText = coordinates.ToString();
        //     Vector2 rotation = transform.rotation.eulerAngles;
        //     string rotationText = rotation.ToString();
        //     Vector2 speed = _rigidbody.velocity;
        //     string speedText = speed.ToString();
        //
        //     _gameplayUI.DisplayDataAboutCharacter(coordinatesText, rotationText, speedText);
        // }
    }
}