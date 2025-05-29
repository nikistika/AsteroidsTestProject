using Characters;
using ConfigData;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Enums;
using LoadingAssets;
using Managers;
using Player;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class UFOFactory : EnemyFactory<UFO>
    {
        private readonly ShipRepository _shipRepository;

        public UFOFactory(
            ScoreService scoreService,
            GameState gameState,
            ScreenSize screenSize,
            ShipRepository shipRepository,
            KillService killService,
            IAssetLoader assetLoader,
            RemoteConfigService remoteConfigService) :
            base(scoreService, gameState, screenSize, killService, assetLoader, remoteConfigService)
        {
            _shipRepository = shipRepository;
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
            UFO.Construct(GameState, _shipRepository, ScreenSize, KillService);
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