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
        private readonly UISpawner _uiSpawner;
        private readonly AnalyticsController _analyticsController;

        public GameEntryPoint(
            SpaceShipSpawner spaceShipSpawner,
            AsteroidSpawner asteroidSpawner,
            UISpawner uiSpawner,
            UfoSpawner ufoSpawner,
            AnalyticsController analyticsController)
        {
            _spaceShipSpawner = spaceShipSpawner;
            _asteroidSpawner = asteroidSpawner;
            _uiSpawner = uiSpawner;
            _ufoSpawner = ufoSpawner;
            _analyticsController = analyticsController;
        }

        public async void Initialize()
        {
            await _spaceShipSpawner.StartWork();
            await _uiSpawner.StartWork();
            // await _asteroidSpawner.StartWork();
            // await _ufoSpawner.StartWork();

            _analyticsController.StartGameEvent();
        }
    }
}