using GameLogic;
using Managers;
using Player;
using Shooting;
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
        // private readonly GameplayUI _gameplayUIPrefab;
        private readonly ShipRepository _shipRepository;
        private readonly GameOver _gameOver;
        private readonly GameplayUIRepository _gameplayUIRepository;
        
        private readonly GameplayUIView _gameplayUIViewPrefab;
        
        private GameplayUIModel _gameplayUIModel;
        private GameplayUIPresenter _gameplayUIPresenter;
        
        public UISpawnManager(ScoreManager scoreManager,
            IInstantiator instantiator, /*GameplayUI gameplayUIPrefab,*/
            ShipRepository shipRepository, GameOver gameOver, GameplayUIRepository gameplayUIRepository,
            
            // GameplayUIModel gameplayUIModel, GameplayUIPresenter gameplayUIPresenter, 
            GameplayUIView gameplayUIViewPrefab)
        {
            _scoreManager = scoreManager;
            _instantiator = instantiator;
            // _gameplayUIPrefab = gameplayUIPrefab;
            _shipRepository = shipRepository;
            _gameOver = gameOver;
            _gameplayUIRepository = gameplayUIRepository;
            
            // _gameplayUIModel = gameplayUIModel;
            // _gameplayUIPresenter = gameplayUIPresenter;
            _gameplayUIViewPrefab = gameplayUIViewPrefab;
        }
        
        public void StartWork()
        {
            SpawnUI();
        }

        //TODO: Перенести логику из GameplayUI Start сюда
        private void SpawnUI()
        {
            var gameplayUIObject = _instantiator.InstantiatePrefab(_gameplayUIViewPrefab);
            var gameplayUIPresenter = gameplayUIObject.GetComponent<GameplayUIView>();

            _gameplayUIModel = new GameplayUIModel();
            _gameplayUIPresenter = new GameplayUIPresenter(gameplayUIPresenter, _gameplayUIModel, _gameOver,
                _shipRepository, _scoreManager);
            
            _gameplayUIRepository.GetGameplayUIObject(gameplayUIPresenter);
            
            _gameplayUIPresenter.StartWork();
            
            // gameplayUI.Construct(_shipRepository, _gameOver, _scoreManager);
        }
        
    }
}