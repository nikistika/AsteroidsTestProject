using System.Threading.Tasks;
using Characters;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Enums;
using LoadingAssets;
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
            [Inject(Id = GameInstallerIDs.AsteroidPoolSizeData)] PoolSizeSO asteroidPoolSizeData,
            KillManager killManager,
            IAssetLoader assetLoader) :
            base(scoreManager, gameOver, screenSize, prefab, asteroidPoolSizeData, killManager, assetLoader)
        {
        }

        protected override async UniTask<Asteroid> ActionCreateObject()
        {
            var asteroid = await _assetLoader.CreateAsteroid();
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