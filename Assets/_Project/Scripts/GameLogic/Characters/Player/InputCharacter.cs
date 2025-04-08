using System;
using GameLogic;
using Shooting;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InputCharacter : MonoBehaviour
    {

        private GameOver _gameOver;
        private Rigidbody2D _rigidbody;
        private bool _flagGameOver;

        [SerializeField] private float _speedMove = 70f;
        [SerializeField] private float _speedRotate = 2f;
        [SerializeField] private ShootingMissile _shootingMissile;
        [SerializeField] private ShootingLaser _shootingLaser;

        public void Construct(GameOver gameOver)
        {
            _gameOver = gameOver;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _gameOver.OnGameOver += GameOver;
        }

        private void Update()
        {
            if (!_flagGameOver)
            {
                Input();
            }
        }

        private void Input()
        {
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                _rigidbody.AddRelativeForce(Vector2.up * (_speedMove * Time.deltaTime), ForceMode2D.Force);
            }

            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                _rigidbody.MoveRotation(_rigidbody.rotation - _speedRotate);
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                _rigidbody.MoveRotation(_rigidbody.rotation + _speedRotate);
            }

            if (UnityEngine.Input.GetKey(KeyCode.Space))
            {
                _shootingMissile.Shot();
            }

            if (UnityEngine.Input.GetKey(KeyCode.G))
            {
                _shootingLaser.Shot();
            }
        }

        private void GameOver()
        {
            _rigidbody.velocity = Vector2.zero;
            _flagGameOver = true;
        }
    }
}