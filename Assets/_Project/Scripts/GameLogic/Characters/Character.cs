using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        protected Rigidbody2D _rigidbody;

        public void Construct(float halfHeightCamera, float halfWidthCamera)
        {
            _halfHeightCamera = halfHeightCamera;
            _halfWidthCamera = halfWidthCamera;
        }
        
        protected void Awake()
        {
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