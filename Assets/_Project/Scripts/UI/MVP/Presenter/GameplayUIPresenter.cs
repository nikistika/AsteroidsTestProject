using GameLogic;
using Managers;
using Player;
using UI.Model;
using UI.View;

namespace UI.Presenter
{
    public class GameplayUIPresenter
    { 
        private GameplayUIView _gameplayUIView;
        private GameplayUIModel _gameplayUIModel;
        
        private GameOver _gameOver;
        
        private int _startScore = 0;
        private int _maxLaserCount = 5;
        private bool _flagLaserTime;

        public GameplayUIPresenter(GameplayUIView gameplayUIView, GameplayUIModel gameplayUIModel, GameOver gameOver, 
            ShipRepository shipRepository, ScoreManager scoreManager)
        {
            _gameplayUIView = gameplayUIView;
            _gameplayUIModel = gameplayUIModel;
            _gameOver = gameOver;
            //TODO: реализовать логику shipRepository и scoreManager
        }

        public void StartWork()
        {
            _gameplayUIModel.CurrentScoreChanged += UpdateCurrentScore;
            _gameplayUIModel.CurrentLaserCountChanged += UpdateCurrentLaserCount;
            _gameplayUIModel.CooldawnLaserCurrentTimeChanged += UpdateCurrentLaserTime;
            _gameplayUIModel.CoordinatesShipChanged += UpdateCoordinates;
            _gameplayUIModel.SpeedShipChanged += UpdateSpeed;
            _gameplayUIModel.RotationShipChanged += UpdateRotation;
            
            _gameOver.OnGameOver += GameOver;
            
            _gameplayUIModel.SetInitialValues(_startScore, _maxLaserCount);
        }

        private void UpdateCurrentScore()
        {
            _gameplayUIView.SetScore($"Score: {_gameplayUIModel.CurrentScore}");
        }
        
        private void UpdateCurrentLaserCount()
        {
            
            _gameplayUIView.SetLaserCount(
                $"Laser: {_gameplayUIModel.CurrentLaserCount}/{_gameplayUIModel.MaxLaserCount}");
        }

        private void UpdateCurrentLaserTime()
        {
            _gameplayUIView.SetLaserCount(
                $"Laser: {_gameplayUIModel.CurrentLaserCount}/{_gameplayUIModel.MaxLaserCount}" +
                $"({_gameplayUIModel.CooldawnLaserCurrentTime}/{_gameplayUIModel.CooldawnLaserMaxTime})"
                );
        }

        private void UpdateCoordinates()
        {
            _gameplayUIView.SetCoordinates($"Coordinates: {_gameplayUIModel.CoordinatesShip}");
        }

        private void UpdateRotation()
        {
            _gameplayUIView.SetRotation($"Rotation: {_gameplayUIModel.RotationShip}");
        }

        private void UpdateSpeed()
        {
            _gameplayUIView.SetSpeed($"Speed: {_gameplayUIModel.SpeedShip}");
        }

        private void GameOver()
        {
            _gameplayUIModel.CurrentScoreChanged -= UpdateCurrentScore;
            _gameplayUIModel.CurrentLaserCountChanged -= UpdateCurrentLaserCount;
            _gameplayUIModel.CooldawnLaserCurrentTimeChanged -= UpdateCurrentLaserTime;
            _gameplayUIModel.CoordinatesShipChanged -= UpdateCoordinates;
            _gameplayUIModel.SpeedShipChanged -= UpdateSpeed;
            _gameplayUIModel.RotationShipChanged -= UpdateRotation;
            
            _gameOver.OnGameOver -= GameOver;
        }
    }
}