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
            [Inject(Id = GameInstallerIDs.AsteroidPoolSizeData)]
            PoolSizeSO asteroidPoolSizeData,
            KillService killService,
            IAssetLoader assetLoader) :
            base(scoreService, gameOver, screenSize, asteroidPoolSizeData, killService, assetLoader)
        {
        }

        protected override Asteroid ActionCreateObject()
        {
            var asteroid = Object.Instantiate(Prefab);
            asteroid.Construct(GameOver, ScreenSize, KillService);
            asteroid.Initialize();
            asteroid.GetComponent<Score>().Initialize(ScoreService);
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

        protected override async UniTask GetPrefab()
        {
            Prefab = await _assetLoader.CreateAsteroid();
        }

        public async void Initialize()
        {
            await StartWork();
        }
    }
}