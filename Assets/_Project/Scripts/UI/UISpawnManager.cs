using GameLogic;
using Managers;
using Player;
using UI.Model;
using UI.Presenter;
using UI.View;
using Zenject;

namespace UI
{
    public class UISpawnManager
    {
        private readonly ScoreManager _scoreManager;
        private readonly IInstantiator _instantiator;
        private readonly ShipRepository _shipRepository;
        private readonly GameOver _gameOver;
        private readonly GameplayUIRepository _gameplayUIRepository;
        private readonly GameplayUIView _gameplayUIViewPrefab;
        private GameplayUIModel _gameplayUIModel;
        private GameplayUIPresenter _gameplayUIPresenter;

        public UISpawnManager(
            ScoreManager scoreManager,
            IInstantiator instantiator,
            ShipRepository shipRepository,
            GameOver gameOver,
            GameplayUIRepository gameplayUIRepository,
            GameplayUIView gameplayUIViewPrefab)
        {
            _scoreManager = scoreManager;
            _instantiator = instantiator;
            _shipRepository = shipRepository;
            _gameOver = gameOver;
            _gameplayUIRepository = gameplayUIRepository;
            _gameplayUIViewPrefab = gameplayUIViewPrefab;
        }

        public void StartWork()
        {
            SpawnUI();
        }

        private void SpawnUI()
        {
            var gameplayUIObject = _instantiator.InstantiatePrefab(_gameplayUIViewPrefab);
            var gameplayUIPresenter = gameplayUIObject.GetComponent<GameplayUIView>();
            _gameplayUIModel = new GameplayUIModel();
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIPresenter, _gameplayUIModel, _gameOver,
                _shipRepository, _scoreManager);

            _gameplayUIRepository.GetGameplayUIObject(gameplayUIPresenter);

            _gameplayUIPresenter.StartWork();
        }
    }
}