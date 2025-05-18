using System;
using GameLogic;
using InputSystem;
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
        private IInput _input;
        private Vector2 _direction;

        [SerializeField] private float _speedMove = 70f;
        [SerializeField] private float _speedRotate = 2f;
        [SerializeField] private ShootingMissile _shootingMissile;
        [SerializeField] private ShootingLaser _shootingLaser;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = GetComponent<InputKeyboard>();
        }
        
        public void Construct(GameOver gameOver)
        {
            _gameOver = gameOver;
        }

        private void Start()
        {
            _gameOver.OnGameOver += GameOver;
            _gameOver.OnContinueGame += GameContinue;
            _gameOver.OnGameExit += GameExit;
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
            if (_input.ButtonForward())
            {
                _rigidbody.AddRelativeForce(Vector2.up * (_speedMove * Time.deltaTime), ForceMode2D.Force);
            }

            if (_input.ButtonRight())
            {
                _rigidbody.MoveRotation(_rigidbody.rotation - _speedRotate);
            }

            if (_input.ButtonLeft())
            {
                _rigidbody.MoveRotation(_rigidbody.rotation + _speedRotate);
            }

            if (_input.ButtonShootingMissile())
            {
                _shootingMissile.Shot();
            }

            if (_input.ButtonShootingLaser())
            {
                _shootingLaser.Shot();
            }
        }

        private void GameOver()
        {
            _rigidbody.velocity = Vector2.zero;
            _flagGameOver = true;
        }

        private void GameContinue()
        {
            _rigidbody.position = Vector2.zero;
            _rigidbody.rotation = 0f;
            _flagGameOver = false;
        }

        private void GameExit()
        {
            _gameOver.OnGameOver -= GameOver;
            _gameOver.OnContinueGame -= GameContinue;
            _gameOver.OnGameExit -= GameExit;
        }
    }
}