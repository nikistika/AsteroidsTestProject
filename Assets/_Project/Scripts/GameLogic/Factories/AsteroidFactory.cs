using Characters;
using GameLogic;
using Managers;
using SciptableObjects;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class AsteroidFactory : EnemyFactory<Asteroid>, IInitializable
    {
        public AsteroidFactory(
            ScoreManager scoreManager,
            GameOver gameOver,
            ScreenSize screenSize,
            Asteroid prefab,
            PoolSizeSO asteroidPoolSizeData,
            KillManager killManager) :
            base(scoreManager, gameOver, screenSize, prefab, asteroidPoolSizeData, killManager)
        {
        }

        protected override Asteroid ActionCreateObject()
        {
            Asteroid asteroid = Object.Instantiate(Prefab);
            asteroid.Construct(GameOver, ScreenSize, _killManager);
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

        public void Initialize()
        {
            StartWork();
        }
    }
}