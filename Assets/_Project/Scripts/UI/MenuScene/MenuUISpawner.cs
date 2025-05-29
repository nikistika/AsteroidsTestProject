using System;
using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData;
using IAP;
using LoadingAssets;
using Managers;
using Object = UnityEngine.Object;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUISpawner : IDisposable
    {
        private readonly IAssetLoader _assetLoader;
        private readonly ISceneService _sceneService;
        private readonly IAPService _iapService;
        private readonly SaveController _saveController;

        private MenuUIPresenter _menuUIPresenter;
        private MenuUIView _menuUIView;

        public MenuUISpawner(
            IAssetLoader assetLoader,
            ISceneService sceneService,
            IAPService iapService,
            SaveController saveController)
        {
            _assetLoader = assetLoader;
            _sceneService = sceneService;
            _iapService = iapService;
            _saveController = saveController;
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
            _menuUIPresenter = new MenuUIPresenter(gameplayUIObject, _sceneService, _iapService, _saveController);
            _menuUIPresenter.StartWork();
        }

        public void Dispose()
        {
            _menuUIPresenter.RemoveListeners();
        }
    }
}