using Characters;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Enums;
using LoadingAssets;
using Managers;
using Player;
using SciptableObjects;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class UFOFactory : EnemyFactory<UFO>, IInitializable
    {
        private readonly ShipRepository _shipRepository; 
        
        public UFOFactory(
            ScoreService scoreService,
            GameOver gameOver,
            ScreenSize screenSize,
            UFO prefab,
            ShipRepository shipRepository,
            [Inject(Id = GameInstallerIDs.UFOPoolSizeData)] PoolSizeSO ufoPoolSizeData,
            KillService killService, 
            IAssetLoader assetLoader) :
            base(scoreService, gameOver, screenSize, prefab, ufoPoolSizeData, killService, assetLoader)
        {
            _shipRepository = shipRepository;
        }

        protected override void ActionReleaseObject(UFO obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }

        protected override async UniTask<UFO> ActionCreateObject()
        {
            var UFO = await _assetLoader.CreateUFO();

            UFO.Construct(GameOver, _shipRepository, ScreenSize, KillService);
            UFO.GetComponent<Score>().Construct(ScoreService);
            UFO.gameObject.transform.position = GetRandomSpawnPosition();
            return UFO;
        }

        protected override void ActionGetObject(UFO obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition();
        }

        public void Initialize()
        {
            StartWork();
        }
    }
}