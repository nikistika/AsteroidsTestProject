using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Shootnig;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.InputSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InputCharacter : MonoBehaviour
    {
        private GameState _gameState;
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

        public void Construct(GameState gameState)
        {
            _gameState = gameState;
        }

        private void Start()
        {
            _gameState.OnGameOver += GameState;
            _gameState.OnGameContinue += GameContinue;
            _gameState.OnGameExit += GameExit;
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
            if (_input.IsPressedForwardButton())
            {
                _rigidbody.AddRelativeForce(Vector2.up * (_speedMove * UnityEngine.Time.deltaTime), ForceMode2D.Force);
            }

            if (_input.IsPressedRightButton())
            {
                _rigidbody.MoveRotation(_rigidbody.rotation - _speedRotate);
            }

            if (_input.IsPressedLeftButton())
            {
                _rigidbody.MoveRotation(_rigidbody.rotation + _speedRotate);
            }

            if (_input.IsPressedMissileShootButton())
            {
                _shootingMissile.Shot();
            }

            if (_input.IsPressedLaserShootButton())
            {
                _shootingLaser.Shot();
            }
        }

        private void GameState()
        {
            _rigidbody.velocity = Vector2.zero;
            _flagGameOver = true;
        }

        private UniTask GameContinue()
        {
            _rigidbody.position = Vector2.zero;
            _rigidbody.rotation = 0f;
            _flagGameOver = false;
            return UniTask.CompletedTask;
        }

        private void GameExit()
        {
            _gameState.OnGameOver -= GameState;
            _gameState.OnGameContinue -= GameContinue;
            _gameState.OnGameExit -= GameExit;
        }
    }
}