using GameLogic;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;

        protected ScreenSize ScreenSize { get; private set; }

        protected void Construct(
            ScreenSize screenSize)
        {
            ScreenSize = screenSize;
        }

        protected void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            GoingAbroad();
        }

        protected void GoingAbroad()
        {
            if (ScreenSize != null)
            {
                if (gameObject.transform.position.y > ScreenSize.HalfHeightCamera + 0.5f)
                {
                    Rigidbody.MovePosition(new Vector2(gameObject.transform.position.x,
                        -ScreenSize.HalfHeightCamera));
                }

                if (gameObject.transform.position.y < -ScreenSize.HalfHeightCamera - 0.5f)
                {
                    Rigidbody.MovePosition(new Vector2(gameObject.transform.position.x,
                        ScreenSize.HalfHeightCamera));
                }

                if (gameObject.transform.position.x > ScreenSize.HalfWidthCamera + 0.5f)
                {
                    Rigidbody.MovePosition(new Vector2(-ScreenSize.HalfWidthCamera,
                        gameObject.transform.position.y));
                }

                if (gameObject.transform.position.x < -ScreenSize.HalfWidthCamera - 0.5f)
                {
                    Rigidbody.MovePosition(new Vector2(ScreenSize.HalfWidthCamera,
                        gameObject.transform.position.y));
                }
            }
        }
    }
}