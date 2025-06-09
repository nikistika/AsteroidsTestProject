using System;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.IAP;
using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData;
using LoadingAssets;
using Object = UnityEngine.Object;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUISpawner : IDisposable
    {
        private readonly IAssetLoader _assetLoader;
        private readonly ISceneService _sceneService;
        private readonly IIAPService _iapService;
        private readonly ILocalSaveService _localSaveService;

        private MenuUIPresenter _menuUIPresenter;
        private MenuUIView _menuUIView;

        public MenuUISpawner(
            IAssetLoader assetLoader,
            ISceneService sceneService,
            IIAPService iapService,
            ILocalSaveService localSaveService)
        {
            _assetLoader = assetLoader;
            _sceneService = sceneService;
            _iapService = iapService;
            _localSaveService = localSaveService;
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
            _menuUIPresenter = new MenuUIPresenter(gameplayUIObject, _sceneService, _iapService, _localSaveService);
            _menuUIPresenter.StartWork();
        }

        public void Dispose()
        {
            _menuUIPresenter.RemoveListeners();
        }
    }
}