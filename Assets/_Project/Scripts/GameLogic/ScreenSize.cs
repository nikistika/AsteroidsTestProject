using UnityEngine;

namespace _Project.Scripts.GameLogic
{
    public class ScreenSize
    {
        private readonly Camera _camera;

        public float HalfHeightCamera { get; private set; }
        public float HalfWidthCamera { get; private set; }

        public ScreenSize(Camera camera)
        {
            _camera = camera;
            HalfHeightCamera = _camera.orthographicSize;
            HalfWidthCamera = HalfHeightCamera * _camera.aspect;
        }
    }
}