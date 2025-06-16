using System;
using _Project.Scripts.Addressable;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.IAP;
using _Project.Scripts.Save;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUIFactory : IDisposable
    {
        private readonly IAssetLoader _assetLoader;
        private readonly ISceneService _sceneService;
        private readonly IIAPService _iapService;
        private readonly ISaveService _saveService;

        private MenuUIPresenter _menuUIPresenter;
        private MenuUIView _menuUIView;

        public MenuUIFactory(
            IAssetLoader assetLoader,
            ISceneService sceneService,
            IIAPService iapService,
            ISaveService saveService)
        {
            _assetLoader = assetLoader;
            _sceneService = sceneService;
            _iapService = iapService;
            _saveService = saveService;
        }

        public async UniTask StartWork()
        {
            await GetPrefab();
            SpawnUI();
        }

        private async UniTask GetPrefab()
        {
            _menuUIView = await _assetLoader.CreateMenuUIView();
        }

        private void SpawnUI()
        {
            var gameplayUIObject = Object.Instantiate(_menuUIView);
            _menuUIPresenter = new MenuUIPresenter(gameplayUIObject, _sceneService, _iapService, _saveService,
                _assetLoader);
            _menuUIPresenter.StartWork();
        }

        public void Dispose()
        {
            _menuUIPresenter.RemoveListeners();
        }
    }
}