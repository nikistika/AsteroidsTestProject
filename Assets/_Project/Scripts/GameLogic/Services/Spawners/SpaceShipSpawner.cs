using _Project.Scripts.Addressable;
using _Project.Scripts.Analytics;
using _Project.Scripts.AnimationControllers;
using _Project.Scripts.Audio;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.GameLogic.Factories;
using _Project.Scripts.GameLogic.Shootnig;
using _Project.Scripts.InputSystem;
using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Services.Spawners
{
    public class SpaceShipSpawner : BaseSpawner<SpaceShip>
    {
        private InputCharacter _inputCharacter;
        private ShootingMissile _shootingMissile;
        private InputKeyboard _inputKeyboard;
        private MissileFactory _missileFactory;
        private SpaceShip _spaceShipPrefab;

        private ShootingLaser _shootingLaser;
        private DataSpaceShip _dataSpaceShip;
        private ShootingAnimationController _shootingAnimationController;

        private readonly ShipRepository _shipRepository;
        private readonly IAnalyticsService _analyticsService;
        private readonly IKillService _killService;
        private readonly IAssetLoader _assetLoader;
        private readonly IRemoteConfigService _remoteConfigService;
        private readonly IAudioService _audioService;

        public SpaceShip SpaceShipObject { get; private set; }

        public SpaceShipSpawner(
            GameState gameState,
            ScreenSize screenSize,
            ShipRepository shipRepository,
            IAnalyticsService analyticsService,
            IKillService killService,
            IAssetLoader assetLoader,
            IRemoteConfigService remoteConfigService,
            IAudioService audioService) :
            base(gameState, screenSize)
        {
            _shipRepository = shipRepository;
            _analyticsService = analyticsService;
            _killService = killService;
            _remoteConfigService = remoteConfigService;
            _assetLoader = assetLoader;
            _audioService = audioService;
        }

        protected override async UniTask Initialize()
        {
            await GetPrefab();
            SpaceShipObject = await SpawnObject();
        }

        private async UniTask<SpaceShip> SpawnObject()
        {
            var objectSpaceShip = Object.Instantiate(_spaceShipPrefab);
            objectSpaceShip.gameObject.SetActive(true);
            GetComponentsSpaceShip(objectSpaceShip);
            await DependencyTransfer(objectSpaceShip);
            return objectSpaceShip;
        }

        protected override UniTask GameContinue()
        {
            FlagGameOver = false;
            return UniTask.CompletedTask;
        }

        private async UniTask GetPrefab()
        {
            _spaceShipPrefab = await _assetLoader.CreateSpaceShip();
        }

        private void GetComponentsSpaceShip(SpaceShip objectSpaceShip)
        {
            _inputCharacter = objectSpaceShip.GetComponent<InputCharacter>();
            _shootingMissile = objectSpaceShip.GetComponent<ShootingMissile>();
            _shootingLaser = objectSpaceShip.GetComponent<ShootingLaser>();
            _dataSpaceShip = objectSpaceShip.GetComponent<DataSpaceShip>();
            _shootingAnimationController = objectSpaceShip.GetComponent<ShootingAnimationController>();
                
            _shipRepository.GetSpaceShip(objectSpaceShip, _shootingLaser, _dataSpaceShip);
        }

        private async UniTask DependencyTransfer(SpaceShip objectSpaceShip)
        {
            objectSpaceShip.Construct(_analyticsService, _killService, ScreenSize, _audioService);
            objectSpaceShip.StartWork();
            _inputCharacter.Construct(GameState);

            _missileFactory = new MissileFactory(ScreenSize, objectSpaceShip, _shootingMissile,
                _assetLoader, _remoteConfigService);
            await _missileFactory.StartWork();

            _shootingMissile.Construct(_missileFactory, _killService, _audioService, _shootingAnimationController);

            _shootingMissile.Initialize();
        }
    }
}