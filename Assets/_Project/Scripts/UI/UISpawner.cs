using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Ads;
using GameLogic.SaveLogic.SaveData;
using LoadingAssets;
using Managers;
using Player;
using UI.Presenter;
using UI.View;
using UnityEngine;

namespace UI
{
    public class UISpawner
    {
        private readonly ScoreService _scoreService;
        private readonly ShipRepository _shipRepository;
        private readonly GameOver _gameOver;
        private GameplayUIPresenter _gameplayUIPresenter;
        private SaveController _saveController;
        private IAssetLoader _assetLoader;
        private readonly AdsController _adsController;
        
        private GameplayUIView _gameplayUIView;

        public UISpawner(
            ScoreService scoreService,
            ShipRepository shipRepository,
            GameOver gameOver,
            SaveController saveController,
            IAssetLoader assetLoader,
            AdsController adsController)
        {
            _scoreService = scoreService;
            _shipRepository = shipRepository;
            _gameOver = gameOver;
            _saveController = saveController;
            _assetLoader = assetLoader;
            _adsController = adsController;
        }

        public async UniTask StartWork()
        {
            await GetPrefab();
            SpawnUI();
        }

        private async UniTask GetPrefab()
        {
            _gameplayUIView = await _assetLoader.CreateGameplayUIView();
        }

        private void SpawnUI()
        {
            var gameplayUIObject = Object.Instantiate(_gameplayUIView);
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIObject, _gameOver,
                _shipRepository, _scoreService, _saveController, _adsController);

            _gameplayUIPresenter.StartWork();
        }
    }
}