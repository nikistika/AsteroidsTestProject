using GameLogic;
using Managers;
using SciptableObjects;
using UnityEngine;

namespace Factories
{
    public abstract class AbstractEnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        protected ScoreManager ScoreManager;
        protected GameOver GameOver;

        protected AbstractEnemyFactory(ScoreManager scoreManager, GameOver gameOver,
            float halfHeightCamera, float halfWidthCamera, T prefab, PoolSizeSO poolSizeData) : 
            base(halfHeightCamera, halfWidthCamera, prefab, poolSizeData)
        {
            GameOver = gameOver;
            ScoreManager = scoreManager;
        }

        protected Vector2 GetRandomSpawnPosition()
        {
            var randomIndex = Random.Range(1, 5);

            switch (randomIndex)
            {
                case 1:
                    return new Vector2(Random.Range(-HalfWidthCamera, HalfWidthCamera), HalfHeightCamera + 0.5f);
                case 2:
                    return new Vector2(Random.Range(-HalfWidthCamera, HalfWidthCamera), -HalfHeightCamera - 0.5f);
                case 3:
                    return new Vector2(HalfWidthCamera + 0.5f, Random.Range(-HalfHeightCamera, HalfHeightCamera));
                case 4:
                    return new Vector2(-HalfWidthCamera - 0.5f, Random.Range(-HalfHeightCamera, HalfHeightCamera));
            }

            return new Vector2(0, 0);
        }
    }
}