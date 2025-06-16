using UnityEngine;

namespace _Project.Scripts.GameLogic.Services
{
    public class RandomService : IRandomService
    {
        private const float SPAWN_OFFSET = 0.5f;
        private const float MIN_SCALE = 1f;
        private const float MEDIUM_SCALE = 1.5f;
        private const float LARGE_SCALE = 2f;
        private const float MAX_SCALE = 2.5f;
        private const float RANDOM_RANGE = 0.5f;
        private const float DIRECTION_UP = -1.0f;
        private const float DIRECTION_DOWN = 1.0f;
        private const float DIRECTION_LEFT = -1.0f;
        private const float DIRECTION_RIGHT = 1.0f;

        private readonly ScreenSize _screenSize;

        public RandomService(
            ScreenSize screenSize)
        {
            _screenSize = screenSize;
        }

        public Vector3 GetRandomScale()
        {
            float xScale;
            float yScale;

            if (Random.value > 0.5f)
            {
                xScale = Random.Range(MIN_SCALE, MEDIUM_SCALE);
                yScale = Random.Range(MIN_SCALE, MEDIUM_SCALE);
            }
            else
            {
                xScale = Random.Range(MEDIUM_SCALE, MAX_SCALE);
                yScale = Random.Range(MEDIUM_SCALE, MAX_SCALE);
            }

            return new Vector3(xScale, yScale, LARGE_SCALE);
        }

        public Vector2 GetRandomDirection(float y, float x)
        {
            if (y > _screenSize.HalfHeightCamera)
                return new Vector2(Random.Range(0, RANDOM_RANGE), DIRECTION_UP);

            if (y < -_screenSize.HalfHeightCamera)
                return new Vector2(Random.Range(0, RANDOM_RANGE), DIRECTION_DOWN);

            if (x > _screenSize.HalfWidthCamera)
                return new Vector2(DIRECTION_LEFT, Random.Range(0, RANDOM_RANGE));

            if (x < -_screenSize.HalfWidthCamera)
                return new Vector2(DIRECTION_RIGHT, Random.Range(0, RANDOM_RANGE));

            return new Vector2(DIRECTION_RIGHT, DIRECTION_RIGHT);
        }

        public Vector2 GetRandomFragmentDirection()
        {
            return Random.insideUnitCircle.normalized;
        }

        public Vector2 GetRandomSpawnPosition()
        {
            int randomIndex = Random.Range(1, 5);
            switch (randomIndex)
            {
                case 1:
                    return new Vector2(Random.Range(-_screenSize.HalfWidthCamera, _screenSize.HalfWidthCamera),
                        _screenSize.HalfHeightCamera + SPAWN_OFFSET);
                case 2:
                    return new Vector2(Random.Range(-_screenSize.HalfWidthCamera, _screenSize.HalfWidthCamera),
                        -_screenSize.HalfHeightCamera - SPAWN_OFFSET);
                case 3:
                    return new Vector2(_screenSize.HalfWidthCamera + SPAWN_OFFSET,
                        Random.Range(-_screenSize.HalfHeightCamera, _screenSize.HalfHeightCamera));
                case 4:
                    return new Vector2(-_screenSize.HalfWidthCamera - SPAWN_OFFSET,
                        Random.Range(-_screenSize.HalfHeightCamera, _screenSize.HalfHeightCamera));
            }

            return Vector2.zero;
        }
    }
}