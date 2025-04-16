using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        protected float HalfHeightCamera;
        protected float HalfWidthCamera;
        protected Rigidbody2D Rigidbody;

        public void Construct(float halfHeightCamera, float halfWidthCamera)
        {
            HalfHeightCamera = halfHeightCamera;
            HalfWidthCamera = halfWidthCamera;
        }
        
        protected void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            GoingAbroad();
        }

        protected void GoingAbroad()
        {
            if (gameObject.transform.position.y > HalfHeightCamera + 0.5f)
            {
                Rigidbody.MovePosition(new Vector2(gameObject.transform.position.x, -HalfHeightCamera));
            }

            if (gameObject.transform.position.y < -HalfHeightCamera - 0.5f)
            {
                Rigidbody.MovePosition(new Vector2(gameObject.transform.position.x, HalfHeightCamera));
            }

            if (gameObject.transform.position.x > HalfWidthCamera + 0.5f)
            {
                Rigidbody.MovePosition(new Vector2(-HalfWidthCamera, gameObject.transform.position.y));
            }

            if (gameObject.transform.position.x < -HalfWidthCamera - 0.5f)
            {
                Rigidbody.MovePosition(new Vector2(HalfWidthCamera, gameObject.transform.position.y));
            }
        }

        protected abstract void Initialize();
    }
}