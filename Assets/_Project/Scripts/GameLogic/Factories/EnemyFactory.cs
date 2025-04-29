using GameLogic;
using Managers;
using SciptableObjects;
using UnityEngine;

namespace Factories
{
    public abstract class EnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        protected readonly  ScoreManager ScoreManager;
        protected readonly  GameOver GameOver;

        protected EnemyFactory(
            ScoreManager scoreManager, 
            GameOver gameOver,
            ScreenSize screenSize, 
            T prefab, 
            PoolSizeSO poolSizeData) :
            base(screenSize, prefab, poolSizeData)
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
                    return new Vector2(Random.Range(-ScreenSize.HalfWidthCamera, ScreenSize.HalfWidthCamera),
                        ScreenSize.HalfHeightCamera + 0.5f);
                case 2:
                    return new Vector2(Random.Range(-ScreenSize.HalfWidthCamera, ScreenSize.HalfWidthCamera),
                        -ScreenSize.HalfHeightCamera - 0.5f);
                case 3:
                    return new Vector2(ScreenSize.HalfWidthCamera + 0.5f,
                        Random.Range(-ScreenSize.HalfHeightCamera, ScreenSize.HalfHeightCamera));
                case 4:
                    return new Vector2(-ScreenSize.HalfWidthCamera - 0.5f,
                        Random.Range(-ScreenSize.HalfHeightCamera, ScreenSize.HalfHeightCamera));
            }

            return new Vector2(0, 0);
        }
    }
}