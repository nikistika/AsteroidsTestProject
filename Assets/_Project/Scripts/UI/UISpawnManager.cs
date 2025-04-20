using GameLogic;
using Managers;
using Player;
using Shooting;
using Zenject;

namespace UI
{
    public class UISpawnManager
    {
        private readonly ScoreManager _scoreManager;
        private readonly IInstantiator _iInstantiator;
        private readonly GameplayUI _gameplayUI;
        private readonly ShootingLaser _shootingLaser;
        private readonly DataSpaceShip _dataSpaceShip;
        private readonly GameOver _gameOver;

        public UISpawnManager(ScoreManager scoreManager,
            IInstantiator iInstantiator, GameplayUI gameplayUI, ShootingLaser shootingLaser,
            DataSpaceShip dataSpaceShip, GameOver gameOver)
        {
            _scoreManager = scoreManager;
            _iInstantiator = iInstantiator;
            _gameplayUI = gameplayUI;
            _shootingLaser = shootingLaser;
            _dataSpaceShip = dataSpaceShip;
            _gameOver = gameOver;
        }
        
        public void StartWork()
        {
            SpawnUI();
        }

        private void SpawnUI()
        {
            var gameplayUIObject = _iInstantiator.InstantiatePrefab(_gameplayUI);
            var gameplayUI = gameplayUIObject.GetComponent<GameplayUI>();
            gameplayUI.Construct(_shootingLaser, _dataSpaceShip, _gameOver, _scoreManager);
        }
    }
}