using _Project.Scripts.Addressable;
using _Project.Scripts.Ads;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.Save;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene
{
    public class GameplayUIFactory
    {
        private readonly IScoreService _scoreService;
        private readonly ShipRepository _shipRepository;
        private readonly GameState _gameState;
        private readonly IAdsService _adsService;
        private readonly ISceneService _sceneService;

        private GameplayUIView _gameplayUIView;
        private GameplayUIPresenter _gameplayUIPresenter;
        private ISaveService _saveService;
        private IAssetLoader _assetLoader;

        public GameplayUIFactory(
            IScoreService scoreService,
            ShipRepository shipRepository,
            GameState gameState,
            ISaveService saveService,
            IAssetLoader assetLoader,
            IAdsService adsService,
            ISceneService sceneService)
        {
            _scoreService = scoreService;
            _shipRepository = shipRepository;
            _gameState = gameState;
            _saveService = saveService;
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
            gameplayUIObject.gameObject.SetActive(true);
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIObject, _gameState,
                _shipRepository, _scoreService, _saveService, _adsService, _sceneService, _assetLoader);

            _gameplayUIPresenter.StartWork();
        }
    }
}