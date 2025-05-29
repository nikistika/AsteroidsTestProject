using ConfigData;
using Cysharp.Threading.Tasks;
using Factories;
using GameLogic.Analytics;
using Managers;
using UI;
using Zenject;

namespace GameLogic
{
    public class GameEntryPoint : IInitializable
    {
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly UfoSpawner _ufoSpawner;
        private readonly SpaceShipSpawner _spaceShipSpawner;
        private readonly GameplayUISpawner _gameplayUISpawner;
        private readonly RemoteConfigService _remoteConfigService;
        private readonly AnalyticsController _analyticsController;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly UFOFactory _ufoFactory;

        public GameEntryPoint(
            SpaceShipSpawner spaceShipSpawner,
            AsteroidSpawner asteroidSpawner,
            GameplayUISpawner gameplayUISpawner,
            UfoSpawner ufoSpawner,
            RemoteConfigService remoteConfigService,
            AsteroidFactory asteroidFactory,
            UFOFactory ufoFactory,
            AnalyticsController analyticsController)
        {
            _spaceShipSpawner = spaceShipSpawner;
            _asteroidSpawner = asteroidSpawner;
            _gameplayUISpawner = gameplayUISpawner;
            _ufoSpawner = ufoSpawner;
            _remoteConfigService = remoteConfigService;
            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;
            _analyticsController = analyticsController;
        }

        public async void Initialize()
        {
            await _remoteConfigService.Initialize();
            await _asteroidFactory.StartWork();
            await _ufoFactory.StartWork();

            await _spaceShipSpawner.StartWork();
            await _gameplayUISpawner.StartWork();
            await UniTask.WhenAll(
                _asteroidSpawner.StartWork(),
                _ufoSpawner.StartWork()
            );

            _analyticsController.StartGameEvent();
        }
    }
}