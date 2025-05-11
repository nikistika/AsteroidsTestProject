using GameLogic;
using GameLogic.SaveLogic.SaveData;
using Managers;
using Player;
using Shooting;
using UI.Model;
using UI.View;

namespace UI.Presenter
{
    public class GameplayUIPresenter
    {
        private readonly GameplayUIView _gameplayUIView;
        private readonly GameplayUIModel _gameplayUIModel;
        private readonly GameOver _gameOver;
        private readonly ShipRepository _shipRepository;
        private readonly ScoreManager _scoreManager;
        private readonly SaveController _saveController;
        
        private ShootingLaser _shootingLaser;
        private int _score;
        private int _recordScore;
        private int _maxLaserCount;
        private bool _flagLaserTime;
        
        public GameplayUIPresenter(
            GameplayUIView gameplayUIView, 
            GameplayUIModel gameplayUIModel, 
            GameOver gameOver,
            ShipRepository shipRepository, 
            ScoreManager scoreManager,
            SaveController saveController)
        {
            _gameplayUIView = gameplayUIView;
            _gameplayUIModel = gameplayUIModel;
            _gameOver = gameOver;
            _shipRepository = shipRepository;
            _scoreManager = scoreManager;
            _saveController = saveController;
        }

        public void StartWork()
        {
            _shootingLaser = _shipRepository.ShootingLaser;

            _gameplayUIModel.CurrentScoreChanged += UpdateCurrentScore;
            _gameplayUIModel.CurrentLaserCountChanged += UpdateCurrentLaserCount;
            _gameplayUIModel.CooldawnLaserCurrentTimeChanged += UpdateCurrentLaserTime;
            _gameplayUIModel.CoordinatesShipChanged += UpdateCoordinates;
            _gameplayUIModel.SpeedShipChanged += UpdateSpeed;
            _gameplayUIModel.RotationShipChanged += UpdateRotation;

            _shootingLaser.OnEditLaserCount += _gameplayUIModel.SetCurrentLaserCount;
            _shootingLaser.OnLaserCooldown += _gameplayUIModel.SetCooldawnLaserCurrentTime;
            _scoreManager.OnScoreChanged += _gameplayUIModel.SetCurrentScore;

            _shipRepository.DataSpaceShip.OnGetCoordinates += _gameplayUIModel.SetCoordinates;
            _shipRepository.DataSpaceShip.OnGetRotation += _gameplayUIModel.SetRotation;
            _shipRepository.DataSpaceShip.OnGetSpeed += _gameplayUIModel.SetSpeed;

            _gameOver.OnGameOver += GameOver;
            _gameOver.OnGameOver += UpdateRecordScore;

            _maxLaserCount = _shootingLaser.MaxLaserCount;
            _score = _gameplayUIModel.CurrentScore;

            _gameplayUIModel.SetInitialValues(_score, _maxLaserCount);
        }

        private void UpdateCurrentScore()
        {
            _score = _gameplayUIModel.CurrentScore;
            _gameplayUIView.SetCurrentScore($"Score: {_gameplayUIModel.CurrentScore}");
        }

        private void UpdateRecordScore()
        {
            _gameplayUIModel.SetRecordScore(_saveController.GetRecord().ScoreRecord);
            _gameplayUIView.SetRecordScore($"Record score: {_gameplayUIModel.RecordScore}");
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

            _gameplayUIView.GameOver(_score);
        }
    }
}