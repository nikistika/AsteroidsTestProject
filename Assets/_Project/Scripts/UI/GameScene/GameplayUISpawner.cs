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
    public class GameplayUISpawner
    {
        private readonly ScoreService _scoreService;
        private readonly ShipRepository _shipRepository;
        private readonly GameState _gameState;
        private readonly AdsController _adsController;
        private readonly ISceneService _sceneService;

        private GameplayUIView _gameplayUIView;
        private GameplayUIPresenter _gameplayUIPresenter;
        private SaveController _saveController;
        private IAssetLoader _assetLoader;
        
        public GameplayUISpawner(
            ScoreService scoreService,
            ShipRepository shipRepository,
            GameState gameState,
            SaveController saveController,
            IAssetLoader assetLoader,
            AdsController adsController,
            ISceneService sceneService)
        {
            _scoreService = scoreService;
            _shipRepository = shipRepository;
            _gameState = gameState;
            _saveController = saveController;
            _assetLoader = assetLoader;
            _adsController = adsController;
            _sceneService = sceneService;
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
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIObject, _gameState,
                _shipRepository, _scoreService, _saveController, _adsController, _sceneService);

            _gameplayUIPresenter.StartWork();
        }
    }
}