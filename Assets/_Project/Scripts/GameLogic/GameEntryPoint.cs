using Cysharp.Threading.Tasks;
using GameLogic.Ads;
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
        private readonly AdsController _adsController;
        private readonly AdsInitializer _adsInitializer;
        
        private bool _initializedAds;
        
        public GameEntryPoint(
            SpaceShipSpawner spaceShipSpawner,
            AsteroidSpawner asteroidSpawner,
            UISpawner uiSpawner,
            UfoSpawner ufoSpawner,
            FirebaseInitializer firebaseInitializer,
            AnalyticsController analyticsController,
            AdsInitializer adsInitializer,
            AdsController adsController)
        {
            _spaceShipSpawner = spaceShipSpawner;
            _asteroidSpawner = asteroidSpawner;
            _uiSpawner = uiSpawner;
            _ufoSpawner = ufoSpawner;
            _analyticsController = analyticsController;
            _firebaseInitializer = firebaseInitializer;
            _adsInitializer = adsInitializer;
            _adsController = adsController;
        }

        public async void Initialize()
        {
            await _adsInitializer.Initialize();
            await _firebaseInitializer.Initialize();
            _adsController.Initialize();
            _adsController.LoadAd();
            _adsController.LoadRewardedAd();
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