using _Project.Scripts.Analytics;
using _Project.Scripts.Audio;
using _Project.Scripts.GameLogic.Factories;
using _Project.Scripts.GameLogic.Services.Spawners;
using _Project.Scripts.RemoteConfig;
using _Project.Scripts.UI.GameScene;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class GameEntryPoint : IInitializable
    {
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly UfoSpawner _ufoSpawner;
        private readonly SpaceShipSpawner _spaceShipSpawner;
        private readonly GameplayUIFactory _gameplayUIFactory;
        private readonly IRemoteConfigService _remoteConfigService;
        private readonly IAnalyticsService _analyticsService;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly UFOFactory _ufoFactory;
        private readonly IAudioService _audioService;

        public GameEntryPoint(
            SpaceShipSpawner spaceShipSpawner,
            AsteroidSpawner asteroidSpawner,
            GameplayUIFactory gameplayUIFactory,
            UfoSpawner ufoSpawner,
            IRemoteConfigService remoteConfigService,
            AsteroidFactory asteroidFactory,
            UFOFactory ufoFactory,
            IAnalyticsService analyticsService,
            IAudioService audioService)
        {
            _spaceShipSpawner = spaceShipSpawner;
            _asteroidSpawner = asteroidSpawner;
            _gameplayUIFactory = gameplayUIFactory;
            _ufoSpawner = ufoSpawner;
            _remoteConfigService = remoteConfigService;
            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;
            _analyticsService = analyticsService;
            _audioService = audioService;
        }

        public async void Initialize()
        {
            await _remoteConfigService.Initialize();
            await _asteroidFactory.StartWork();
            await _ufoFactory.StartWork();

            await _spaceShipSpawner.StartWork();
            await _gameplayUIFactory.StartWork();
            
            await _audioService.Initialize();

            await UniTask.WhenAll(
                _asteroidSpawner.StartWork(),
                _ufoSpawner.StartWork()
            );

            _analyticsService.StartGameEvent();
        }
    }
}