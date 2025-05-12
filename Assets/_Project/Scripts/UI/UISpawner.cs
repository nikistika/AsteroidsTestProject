using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.SaveLogic.SaveData;
using LoadingAssets;
using Managers;
using Player;
using UI.Presenter;

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

        public UISpawner(
            ScoreService scoreService,
            ShipRepository shipRepository,
            GameOver gameOver,
            SaveController saveController,
            IAssetLoader assetLoader)
        {
            _scoreService = scoreService;
            _shipRepository = shipRepository;
            _gameOver = gameOver;
            _saveController = saveController;
            _assetLoader = assetLoader;
        }

        public async void StartWork()
        {
            await SpawnUI();
        }

        private async UniTask SpawnUI()
        {
            var gameplayUIObject = await _assetLoader.CreateGameplayUIView();
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIObject, _gameOver,
                _shipRepository, _scoreService, _saveController);

            _gameplayUIPresenter.StartWork();
        }
    }
}