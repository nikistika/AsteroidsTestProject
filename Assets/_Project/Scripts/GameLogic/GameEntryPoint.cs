using ConfigData;
using Cysharp.Threading.Tasks;
using Factories;
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
        private readonly RemoteConfigController _remoteConfigController;
        private readonly AsteroidFactory _asteroidFactory;
        private readonly UFOFactory _ufoFactory;

        private bool _initializedAds;

        public GameEntryPoint(
            SpaceShipSpawner spaceShipSpawner,
            AsteroidSpawner asteroidSpawner,
            UISpawner uiSpawner,
            UfoSpawner ufoSpawner,
            FirebaseInitializer firebaseInitializer,
            AnalyticsController analyticsController,
            AdsInitializer adsInitializer,
            AdsController adsController,
            RemoteConfigController remoteConfigController,
            AsteroidFactory asteroidFactory,
            UFOFactory ufoFactory)
        {
            _spaceShipSpawner = spaceShipSpawner;
            _asteroidSpawner = asteroidSpawner;
            _uiSpawner = uiSpawner;
            _ufoSpawner = ufoSpawner;
            _analyticsController = analyticsController;
            _firebaseInitializer = firebaseInitializer;
            _adsInitializer = adsInitializer;
            _adsController = adsController;
            _remoteConfigController = remoteConfigController;
            _asteroidFactory = asteroidFactory;
            _ufoFactory = ufoFactory;
        }

        public async void Initialize()
        {
            await _adsInitializer.Initialize();
            await _firebaseInitializer.Initialize();
            await _remoteConfigController.Initialize();
            _adsController.Initialize();
            _adsController.LoadInterstitialAd();
            _adsController.LoadRewardedAd();
            _analyticsController.StartGameEvent();

            await _asteroidFactory.StartWork();
            await _ufoFactory.StartWork();

            await _spaceShipSpawner.StartWork();
            await _uiSpawner.StartWork();
            await UniTask.WhenAll(
                _asteroidSpawner.StartWork(),
                _ufoSpawner.StartWork()
            );
        }
    }
}