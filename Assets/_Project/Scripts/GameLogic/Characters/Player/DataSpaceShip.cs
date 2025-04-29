using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DataSpaceShip : MonoBehaviour
    {
        public event Action<Vector2> OnGetCoordinates;
        public event Action<float> OnGetRotation;
        public event Action<Vector2> OnGetSpeed;
        
        private Rigidbody2D _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            GetCoordinates();
            GetRotation();
            GetSpeed();
        }

        private void GetCoordinates()
        {
            var coordinates = transform.position;
            OnGetCoordinates?.Invoke(coordinates);
        }

        private void GetRotation()
        {
            var rotation = transform.eulerAngles.z;
            OnGetRotation?.Invoke(rotation);
        }

        private void GetSpeed()
        {
            var speed = _rigidbody.velocity;
            OnGetSpeed?.Invoke(speed);
        }
    }
}