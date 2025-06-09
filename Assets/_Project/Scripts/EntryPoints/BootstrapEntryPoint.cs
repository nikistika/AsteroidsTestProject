using _Project.Scripts.Ads;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.Save;
using _Project.Scripts.Save.CloudSave;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly IAdsService _adsService;
        private readonly ISceneService _sceneService;
        private readonly ICloudSaveService _cloudSaveService;
        private readonly ISaveService _saveService;

        public BootstrapEntryPoint(
            IAdsService adsService,
            ISceneService sceneService,
            ICloudSaveService cloudSaveService,
            ISaveService saveService)
        {
            _adsService = adsService;
            _sceneService = sceneService;
            _cloudSaveService = cloudSaveService;
            _saveService = saveService;
        }

        public async void Initialize()
        {
            await _cloudSaveService.Initialize();
            await _saveService.Initialize();
            _adsService.LoadInterstitialAd();
            _adsService.LoadRewardedAd();

            _sceneService.GoToMenu();
        }
    }
}