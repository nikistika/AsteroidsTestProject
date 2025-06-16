using _Project.Scripts.Addressable;
using _Project.Scripts.Enums;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.IAP;
using _Project.Scripts.Save;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUIPresenter
    {
        private readonly ISceneService _sceneService;
        private readonly MenuUIView _menuUIView;
        private readonly IIAPService _iapService;
        private readonly ISaveService _saveService;
        private readonly GameState _gameState;
        private readonly IAssetLoader _assetLoader;

        public MenuUIPresenter(
            MenuUIView menuUIView,
            ISceneService sceneService,
            IIAPService iapService,
            ISaveService saveService,
            IAssetLoader assetLoader)
        {
            _menuUIView = menuUIView;
            _sceneService = sceneService;
            _iapService = iapService;
            _saveService = saveService;
            _assetLoader = assetLoader;
        }

        public void StartWork()
        {
            _menuUIView.OnStartGame += StartGame;
            _menuUIView.OnRemoveAds += RemoveAds;
            _menuUIView.OnQuitGame += QuitGame;
            _saveService.OnSaveDataChanged += HideAdsButton;
            
            HideAdsButton();
        }

        public void RemoveListeners()
        {
            _menuUIView.OnStartGame -= StartGame;
            _menuUIView.OnRemoveAds -= RemoveAds;
            _saveService.OnSaveDataChanged -= HideAdsButton;
            _menuUIView.OnQuitGame -= QuitGame;
        }

        private void StartGame()
        {
            _assetLoader.DestroyMenuUIView();
            _sceneService.GoToGame();
        }

        private void RemoveAds()
        {
            _iapService.MakePurchase(IAPID.RemoveAds.ToString());
        }

        private void HideAdsButton()
        {
            if (_saveService.CurrentSaveData.AdsRemoved)
            {
                _menuUIView.HideRemoveAdsButton();
            }
        }

        private void QuitGame()
        {
            _sceneService.QuitGame();
        }
    }
}