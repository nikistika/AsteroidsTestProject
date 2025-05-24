using Characters;
using ConfigData;
using Cysharp.Threading.Tasks;
using GameLogic;
using LoadingAssets;
using Managers;
using UnityEngine;

namespace Factories
{
    public class AsteroidFactory : EnemyFactory<Asteroid>
    {
        public AsteroidFactory(
            ScoreService scoreService,
            GameState gameState,
            ScreenSize screenSize,
            KillService killService,
            IAssetLoader assetLoader,
            RemoteConfigController remoteConfigController) :
            base(scoreService, gameState, screenSize, killService, assetLoader,
                remoteConfigController)
        {
        }

        protected override Asteroid ActionCreateObject()
        {
            var asteroid = Object.Instantiate(Prefab);
            asteroid.Construct(GameState, ScreenSize, KillService);
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

        protected override void InitializeFactory()
        {
            DefaultPoolSize = RemoteConfigController.AsteroidPoolSizeData.DefaultPoolSize;
            MaxPoolSize = RemoteConfigController.AsteroidPoolSizeData.MaxPoolSize;
        }

        protected override void ActionReleaseObject(Asteroid obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }

        protected override async UniTask GetPrefab()
        {
            Prefab = await AssetLoader.CreateAsteroid();
        }
    }
}