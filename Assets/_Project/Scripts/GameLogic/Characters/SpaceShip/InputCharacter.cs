using Characters;
using UnityEngine;

namespace GameLogic
{
    public class InputCharacter : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private bool _flagGameOver = false;
        
        [SerializeField] private float _speedMove = 70f;
        [SerializeField] private float _speedRotate = 2f;
        [SerializeField] private SpaceShip _spaceShip;
        [SerializeField] private GameOver _gameOver;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
                _spaceShip.ShootingMissile();
            }

            if (UnityEngine.Input.GetKey(KeyCode.G))
            {
                _spaceShip.ShootingLaser();
            }
        }

        private void GameOver()
        {
            _rigidbody.velocity = Vector2.zero;
            _flagGameOver = true;
        }
    }
}