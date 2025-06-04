using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Ads;
using GameLogic.SaveLogic.SaveData;
using LoadingAssets;
using Service;
using Player;
using UI.Presenter;
using UI.View;
using UnityEngine;

namespace UI
{
    public class GameplayUISpawner
    {
        private readonly IScoreService _scoreService;
        private readonly ShipRepository _shipRepository;
        private readonly GameState _gameState;
        private readonly AdsService _adsService;
        private readonly ISceneService _sceneService;

        private GameplayUIView _gameplayUIView;
        private GameplayUIPresenter _gameplayUIPresenter;
        private ILocalSaveService _localSaveService;
        private IAssetLoader _assetLoader;
        
        public GameplayUISpawner(
            IScoreService scoreService,
            ShipRepository shipRepository,
            GameState gameState,
            ILocalSaveService localSaveService,
            IAssetLoader assetLoader,
            AdsService adsService,
            ISceneService sceneService)
        {
            _scoreService = scoreService;
            _shipRepository = shipRepository;
            _gameState = gameState;
            _localSaveService = localSaveService;
            _assetLoader = assetLoader;
            _adsService = adsService;
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
                _shipRepository, _scoreService, _localSaveService, _adsService, _sceneService);

            _gameplayUIPresenter.StartWork();
        }
    }
}