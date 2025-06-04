using GameLogic.Ads;
using GameLogic.Analytics;
using SaveLogic;
using Service;
using Zenject;

namespace EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly IAdsService _adsService;
        private readonly ISceneService _sceneService;
        private readonly ICloudSaveService _cloudSaveService;

        public BootstrapEntryPoint(
            IAdsService adsService,
            ISceneService sceneService,
            ICloudSaveService cloudSaveService)
        {
            _adsService = adsService;
            _sceneService = sceneService;
            _cloudSaveService = cloudSaveService;
        }
        
        public async void Initialize()
        {
            await _cloudSaveService.Initialize();
            _adsService.LoadInterstitialAd();
            _adsService.LoadRewardedAd();
            
            _sceneService.GoToMenu();
        }
    }
}