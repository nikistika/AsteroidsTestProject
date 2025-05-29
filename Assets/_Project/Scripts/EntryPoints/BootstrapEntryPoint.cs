using GameLogic.Ads;
using GameLogic.Analytics;
using Managers;
using Zenject;

namespace EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly AdsController _adsController;
        private readonly ISceneService _sceneService;

        public BootstrapEntryPoint(
            AdsController adsController,
            ISceneService sceneService)
        {
            _adsController = adsController;
            _sceneService = sceneService;
        }
        
        public void Initialize()
        {
            _adsController.LoadInterstitialAd();
            _adsController.LoadRewardedAd();
            
            _sceneService.NextScene();
        }
    }
}