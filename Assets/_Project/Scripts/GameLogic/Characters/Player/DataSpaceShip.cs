using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DataSpaceShip : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Vector2 GetCoordinates()
        {
            var coordinates = transform.position;
            return coordinates;
        }

        public float GetRotation()
        {
            var rotation = transform.eulerAngles.z;
            return rotation;
        }

        public Vector2 GetSpeed()
        {
            if (_rigidbody != null)
            {
                var speed = _rigidbody.velocity;
                return speed;
            }

            return Vector2.zero;
        }
    }
}