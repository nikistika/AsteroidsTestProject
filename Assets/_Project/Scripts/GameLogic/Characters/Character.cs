using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        protected Camera _camera;
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        protected Rigidbody2D _rigidbody;
    }
}