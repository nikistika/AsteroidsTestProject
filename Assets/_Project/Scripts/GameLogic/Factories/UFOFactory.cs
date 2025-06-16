using _Project.Scripts.Addressable;
using _Project.Scripts.AnimationControllers;
using _Project.Scripts.Audio;
using _Project.Scripts.Characters;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Factories
{
    public class UFOFactory : EnemyFactory<UFO>
    {
        private readonly ShipRepository _shipRepository;
        private readonly IAudioService _audioService;

        public UFOFactory(
            IScoreService scoreService,
            GameState gameState,
            ScreenSize screenSize,
            ShipRepository shipRepository,
            IKillService killService,
            IAssetLoader assetLoader,
            IRemoteConfigService remoteConfigService,
            IRandomService randomService,
            IAudioService audioService) :
            base(scoreService, gameState, screenSize, killService, assetLoader, remoteConfigService, randomService)
        {
            _shipRepository = shipRepository;
            _audioService = audioService;
        }

        protected override void InitializeFactory()
        {
            DefaultPoolSize = RemoteConfigService.UfoPoolSizeConfig.DefaultPoolSize;
            MaxPoolSize = RemoteConfigService.UfoPoolSizeConfig.MaxPoolSize;
        }

        protected override void ActionReleaseObject(UFO obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }

        protected override UFO ActionCreateObject()
        {
            var UFO = Object.Instantiate(Prefab);
            var _animationController = UFO.GetComponent<EnemyAnimationController>();
            UFO.Construct(GameState, _shipRepository, ScreenSize, KillService, _audioService, _animationController);
            UFO.Initialize();
            UFO.GetComponent<Score>().Initialize(ScoreService);
            UFO.gameObject.transform.position = GetRandomSpawnPosition();
            return UFO;
        }

        protected override void ActionGetObject(UFO obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition();
        }

        protected override async UniTask GetPrefab()
        {
            Prefab = await AssetLoader.CreateUFO();
        }
    }
}