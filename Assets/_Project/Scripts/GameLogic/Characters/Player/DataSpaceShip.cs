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
            Vector2 coordinates = transform.position;
            return coordinates;
        }
               
        public Vector2 GetRotation()
        {
            Vector2 rotation = transform.rotation.eulerAngles;
            return rotation;
        }
                
        public Vector2 GetSpeed()
        {
            Vector2 speed = _rigidbody.velocity;
            return speed;
        }
    }
}