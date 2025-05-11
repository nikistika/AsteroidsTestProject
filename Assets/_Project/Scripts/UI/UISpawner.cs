using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.SaveLogic.SaveData;
using LoadingAssets;
using Managers;
using Player;
using UI.Model;
using UI.Presenter;
using UI.View;
using Zenject;

namespace UI
{
    public class UISpawner
    {
        private readonly ScoreManager _scoreManager;
        private readonly ShipRepository _shipRepository;
        private readonly GameOver _gameOver;
        private GameplayUIModel _gameplayUIModel;
        private GameplayUIPresenter _gameplayUIPresenter;
        private SaveController _saveController;
        private IAssetLoader _assetLoader;

        public UISpawner(
            ScoreManager scoreManager,
            ShipRepository shipRepository,
            GameOver gameOver,
            SaveController saveController,
            IAssetLoader assetLoader)
        {
            _scoreManager = scoreManager;
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
            _gameplayUIModel = new GameplayUIModel();
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIObject, _gameplayUIModel, _gameOver,
                _shipRepository, _scoreManager, _saveController);
            
            _gameplayUIPresenter.StartWork();
        }
    }
}