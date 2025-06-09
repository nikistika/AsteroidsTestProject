using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace _Project.Scripts.GameLogic.Services
{
    public interface IRandomService
    {
        public Vector3 GetRandomScale();
        public Vector2 GetRandomDirection(float y, float x);
        public Vector2 GetRandomFragmentDirection();
        public Vector2 GetRandomSpawnPosition();
    }
}