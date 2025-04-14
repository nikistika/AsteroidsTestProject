using Characters;
using GameLogic;
using SciptableObjects;
using UnityEngine;

namespace Factories
{
    public class AsteroidFactory : AbstractEnemyFactory<Asteroid>
    {
        public AsteroidFactory(ScoreManager scoreManager, GameOver gameOver, Camera camera, 
            float halfHeightCamera, float halfWidthCamera, Asteroid prefab, PoolSizeSO asteroidPoolSizeData) : 
            base(scoreManager, gameOver, camera, halfHeightCamera, halfWidthCamera, prefab, asteroidPoolSizeData)
        {
        }

        protected override Asteroid ActionCreateObject()
        {
            Asteroid asteroid = Object.Instantiate(Prefab);
            asteroid.Construct(GameOver, HalfHeightCamera, HalfWidthCamera);
            asteroid.GetComponent<Score>().Construct(ScoreManager);
            asteroid.gameObject.transform.position = GetRandomSpawnPosition();
            return asteroid;
        }

        protected override void ActionGetObject(Asteroid obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition();
            obj.Move();
            obj.IsObjectParent(true);
        }

        protected override void ActionReleaseObject(Asteroid obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }
    }
}