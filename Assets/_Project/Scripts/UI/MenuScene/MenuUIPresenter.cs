using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.IAP;
using GameLogic;
using GameLogic.SaveLogic.SaveData;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUIPresenter
    {
        private readonly ISceneService _sceneService;
        private readonly MenuUIView _menuUIView;
        private readonly IIAPService _iapService;
        private readonly ILocalSaveService _localSaveService;
        private readonly GameState _gameState;

        public MenuUIPresenter(
            MenuUIView menuUIView,
            ISceneService sceneService,
            IIAPService iapService,
            ILocalSaveService localSaveService)
        {
            _menuUIView = menuUIView;
            _sceneService = sceneService;
            _iapService = iapService;
            _localSaveService = localSaveService;
        }

        public void StartWork()
        {
            _menuUIView.OnStartGame += StartGame;
            _menuUIView.OnRemoveAds += RemoveAds;
            _localSaveService.OnSaveDataChanged += HideAdsButton;

            HideAdsButton();
        }

        public void RemoveListeners()
        {
            _menuUIView.OnStartGame -= StartGame;
            _menuUIView.OnRemoveAds -= RemoveAds;
            _localSaveService.OnSaveDataChanged -= HideAdsButton;
        }

        private void StartGame()
        {
            _sceneService.GoToGame();
        }

        private void RemoveAds()
        {
            _iapService.RemoveAds();
        }

        private void HideAdsButton()
        {
            if (_localSaveService.GetData().AdsRemoved)
            {
                _menuUIView.HideRemoveAdsButton();
            }
        }
    }
}