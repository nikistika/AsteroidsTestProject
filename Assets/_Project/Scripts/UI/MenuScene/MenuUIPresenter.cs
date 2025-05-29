using GameLogic;
using GameLogic.SaveLogic.SaveData;
using IAP;
using Managers;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUIPresenter
    {
        private readonly ISceneService _sceneService;
        private readonly MenuUIView _menuUIView;
        private readonly IAPService _iapService;
        private readonly SaveController _saveController;
        private readonly GameState _gameState;

        public MenuUIPresenter(
            MenuUIView menuUIView,
            ISceneService sceneService,
            IAPService iapService,
            SaveController saveController)
        {
            _menuUIView = menuUIView;
            _sceneService = sceneService;
            _iapService = iapService;
            _saveController = saveController;
        }

        public void StartWork()
        {
            _menuUIView.OnStartGame += StartGame;
            _menuUIView.OnRemoveAds += RemoveAds;
            _saveController.OnSaveDataChanged += HideAdsButton;

            HideAdsButton();
        }

        public void RemoveListeners()
        {
            _menuUIView.OnStartGame -= StartGame;
            _menuUIView.OnRemoveAds -= RemoveAds;
            _saveController.OnSaveDataChanged -= HideAdsButton;
        }

        private void StartGame()
        {
            _sceneService.NextScene();
        }

        private void RemoveAds()
        {
            _iapService.RemoveAds();
        }

        private void HideAdsButton()
        {
            if (_saveController.GetData().AdsRemoved)
            {
                _menuUIView.HideRemoveAdsButton();
            }
        }
    }
}