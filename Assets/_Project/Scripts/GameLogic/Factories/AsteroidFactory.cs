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
            ScoreService scoreService,
            GameOver gameOver,
            ScreenSize screenSize,
            Asteroid prefab,
            [Inject(Id = GameInstallerIDs.AsteroidPoolSizeData)] PoolSizeSO asteroidPoolSizeData,
            KillService killService,
            IAssetLoader assetLoader) :
            base(scoreService, gameOver, screenSize, prefab, asteroidPoolSizeData, killService, assetLoader)
        {
        }

        protected override async UniTask<Asteroid> ActionCreateObject()
        {
            var asteroid = await _assetLoader.CreateAsteroid();
            asteroid.Construct(GameOver, ScreenSize, KillService);
            asteroid.GetComponent<Score>().Construct(ScoreService);
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