using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        protected Camera _camera;
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        protected Rigidbody2D _rigidbody;

        protected void Awake()
        {
            _camera = Camera.main;
            _halfHeightCamera = _camera.orthographicSize;
            _halfWidthCamera = _halfHeightCamera * _camera.aspect;

            Initialization();
        }

        private void FixedUpdate()
        {
            GoingAbroad();
        }

        protected void GoingAbroad()
        {
            if (gameObject.transform.position.y > _halfHeightCamera + 0.5f)
            {
                _rigidbody.MovePosition(new Vector2(gameObject.transform.position.x, -_halfHeightCamera));
            }

            if (gameObject.transform.position.y < -_halfHeightCamera - 0.5f)
            {
                _rigidbody.MovePosition(new Vector2(gameObject.transform.position.x, _halfHeightCamera));
            }

            if (gameObject.transform.position.x > _halfWidthCamera + 0.5f)
            {
                _rigidbody.MovePosition(new Vector2(-_halfWidthCamera, gameObject.transform.position.y));
            }

            if (gameObject.transform.position.x < -_halfWidthCamera - 0.5f)
            {
                _rigidbody.MovePosition(new Vector2(_halfWidthCamera, gameObject.transform.position.y));
            }
        }

        protected abstract void Initialization();
    }
}