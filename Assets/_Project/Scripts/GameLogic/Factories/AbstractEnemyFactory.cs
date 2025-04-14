using GameLogic;
using UnityEngine;

namespace Factories
{
    public abstract class AbstractEnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        protected ScoreManager _scoreManager;
        protected GameOver _gameOver;
        
        public AbstractEnemyFactory(ScoreManager scoreManager, GameOver gameOver, 
            Camera camera, float halfHeightCamera, float halfWidthCamera, T prefab) : 
            base(camera, halfHeightCamera, halfWidthCamera, prefab)
        {
            _gameOver = gameOver;
            _scoreManager = scoreManager;
        }

        protected Vector2 GetRandomSpawnPosition()
        {
            var randomIndex = Random.Range(1, 5);

            switch (randomIndex)
            {
                case 1:
                    return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), _halfHeightCamera + 0.5f);
                case 2:
                    return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), -_halfHeightCamera - 0.5f);
                case 3:
                    return new Vector2(_halfWidthCamera + 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
                case 4:
                    return new Vector2(-_halfWidthCamera - 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
            }

            return new Vector2(0, 0);
        }
    }
}