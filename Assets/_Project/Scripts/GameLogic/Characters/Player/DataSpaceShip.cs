using System;
using UnityEngine;

namespace Player
{
    public class DataSpaceShip : MonoBehaviour
    {
        
        public event Action<int> OnScoreChanged;

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
            OnScoreChanged.Invoke(CurrentScore);
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