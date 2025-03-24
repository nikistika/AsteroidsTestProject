using UnityEngine;

namespace GameLogic
{
    public class DataSpaceShip : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        public int CurrentScore { get; private set; }
        
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            AddScore(0);
        }
        
        public void AddScore(int score)
        {
            CurrentScore += score;
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