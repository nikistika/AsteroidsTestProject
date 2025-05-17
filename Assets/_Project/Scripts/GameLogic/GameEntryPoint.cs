using Cysharp.Threading.Tasks;
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
        private readonly FirebaseInitializer _firebaseInitializer;

        public GameEntryPoint(
            SpaceShipSpawner spaceShipSpawner,
            AsteroidSpawner asteroidSpawner,
            UISpawner uiSpawner,
            UfoSpawner ufoSpawner,
            FirebaseInitializer firebaseInitializer,
            AnalyticsController analyticsController)
        {
            _spaceShipSpawner = spaceShipSpawner;
            _asteroidSpawner = asteroidSpawner;
            _uiSpawner = uiSpawner;
            _ufoSpawner = ufoSpawner;
            _analyticsController = analyticsController;
            _firebaseInitializer = firebaseInitializer;
        }

        public async void Initialize()
        {
            await _firebaseInitializer.Initialize();
            await _spaceShipSpawner.StartWork();
            await _uiSpawner.StartWork();

            await UniTask.WhenAll(
                _asteroidSpawner.StartWork(),
                _ufoSpawner.StartWork()
            );

            _analyticsController.StartGameEvent();
        }
    }
}