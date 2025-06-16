using _Project.Scripts.Addressable;
using _Project.Scripts.AnimationControllers;
using _Project.Scripts.Audio;
using _Project.Scripts.Characters;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Factories
{
    public class AsteroidFactory : EnemyFactory<Asteroid>
    {
        private readonly IAudioService _audioService;

        public AsteroidFactory(
            IScoreService scoreService,
            GameState gameState,
            ScreenSize screenSize,
            IKillService killService,
            IAssetLoader assetLoader,
            IRemoteConfigService remoteConfigService,
            IRandomService randomService,
            IAudioService audioService) :
            base(scoreService, gameState, screenSize, killService, assetLoader,
                remoteConfigService, randomService)
        {
            _audioService = audioService;
        }

        protected override Asteroid ActionCreateObject()
        {
            var asteroid = Object.Instantiate(Prefab);
            var animationsController = asteroid.GetComponent<EnemyAnimationController>();
            asteroid.Construct(GameState, ScreenSize, KillService, RandomService, _audioService, animationsController);
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
            DefaultPoolSize = RemoteConfigService.AsteroidPoolSizeConfig.DefaultPoolSize;
            MaxPoolSize = RemoteConfigService.AsteroidPoolSizeConfig.MaxPoolSize;
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