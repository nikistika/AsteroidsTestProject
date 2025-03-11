using System.Collections;
using UnityEngine;

namespace GameLogic
{
    public class SpaceShip : MonoBehaviour
    {

        private Rigidbody2D _rigidbody;
        private Camera _camera;

        private float _halfHeightCamera;
        private float _halfWidthCamera;

        private Shooting _shooting;
        
        [SerializeField] private float _speedMove = 70f;
        [SerializeField] private float _speedRotate = 2f;
        [SerializeField] private RestartPanel _restartPanel;
        [SerializeField] private GameplayUI _gameplayUI;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;
            _shooting = gameObject.GetComponent<Shooting>();
        }

        private void Update()
        {
            Move();
            GoingAbroad();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Asteroid>())
            {
                _restartPanel.ActivateRestartPanel(_gameplayUI.CurrentScore);
            }
        }
        
        private void Move()
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
                Shooting();
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

        private void Shooting()
        {
            _shooting.Shot();
        }
        
    }
}