using GameLogic;
using GameLogic.SaveLogic.SaveData;
using Managers;
using Player;
using Shooting;
using UI.View;
using UnityEngine;

namespace UI.Presenter
{
    public class GameplayUIPresenter
    {
        private readonly GameplayUIView _gameplayUIView;
        private readonly GameOver _gameOver;
        private readonly ShipRepository _shipRepository;
        private readonly ScoreService _scoreService;
        private readonly SaveController _saveController;

        private ShootingLaser _shootingLaser;
        private int _currentScore;
        private int _recordScore;
        private int _currentLaserCount;
        private int _maxLaserCount;
        private int _cooldawnLaserCurrentTime;
        private int _cooldawnLaserMaxTime;
        private Vector2 _coordinatesShip;
        private float _rotationShip;
        private Vector2 _speedShip;
        private bool _flagLaserTime;

        public GameplayUIPresenter(
            GameplayUIView gameplayUIView,
            GameOver gameOver,
            ShipRepository shipRepository,
            ScoreService scoreService,
            SaveController saveController)
        {
            _gameplayUIView = gameplayUIView;
            _gameOver = gameOver;
            _shipRepository = shipRepository;
            _scoreService = scoreService;
            _saveController = saveController;
        }

        public void StartWork()
        {
            _shootingLaser = _shipRepository.ShootingLaser;

            _scoreService.OnScoreChanged += UpdateCurrentScore;
            _shootingLaser.OnEditLaserCount += UpdateCurrentLaserCount;
            _shootingLaser.OnLaserCooldown += UpdateCurrentLaserTime;

            _shipRepository.DataSpaceShip.OnGetCoordinates += UpdateCoordinates;
            _shipRepository.DataSpaceShip.OnGetRotation += UpdateRotation;
            _shipRepository.DataSpaceShip.OnGetSpeed += UpdateSpeed;

            _gameOver.OnGameOver += GameOver;
            _gameOver.OnGameOver += UpdateRecordScore;

            _maxLaserCount = _shootingLaser.MaxLaserCount;
            _currentLaserCount = _maxLaserCount;
            _currentScore = 0;
        }

        private void UpdateCurrentScore(int currentScore)
        {
            _currentScore = currentScore;
            _gameplayUIView.SetCurrentScore($"Score: {_currentScore}");
        }

        private void UpdateRecordScore()
        {
            _recordScore = _saveController.GetRecord().ScoreRecord;
            _gameplayUIView.SetRecordScore($"Record score: {_recordScore}");
        }

        private void UpdateCurrentLaserCount(int currentLaserCount)
        {
            _currentLaserCount = currentLaserCount;
            _gameplayUIView.SetLaserCount(
                $"Laser: {_currentLaserCount}/{_maxLaserCount}");
        }

        private void UpdateCurrentLaserTime(int cooldawnLaserCurrentTime, int cooldawnLaserMaxTime)
        {
            _cooldawnLaserCurrentTime = cooldawnLaserCurrentTime;
            _cooldawnLaserMaxTime = cooldawnLaserMaxTime;
            _gameplayUIView.SetLaserCount(
                $"Laser: {_currentLaserCount}/{_maxLaserCount}" +
                $"({_cooldawnLaserCurrentTime}/{_cooldawnLaserMaxTime})"
            );
        }

        private void UpdateCoordinates(Vector2 shipCoordinates)
        {
            _coordinatesShip = shipCoordinates;
            _gameplayUIView.SetCoordinates($"Coordinates: {_coordinatesShip}");
        }

        private void UpdateRotation(float shipRotation)
        {
            _rotationShip = shipRotation;
            _gameplayUIView.SetRotation($"Rotation: {_rotationShip}");
        }

        private void UpdateSpeed(Vector2 speed)
        {
            _speedShip = speed;
            _gameplayUIView.SetSpeed($"Speed: {_speedShip}");
        }

        private void OpenRestartPanel()
        {
            _gameplayUIView.SetCurrentScore($"Score {_currentScore}");
            _gameplayUIView.SetRecordScore($"Record score: {_recordScore}");
            _gameplayUIView.OpenRestartPanel();
        }

        private void GameOver()
        {
            _scoreService.OnScoreChanged -= UpdateCurrentScore;
            _shootingLaser.OnEditLaserCount -= UpdateCurrentLaserCount;
            _shootingLaser.OnLaserCooldown -= UpdateCurrentLaserTime;

            _shipRepository.DataSpaceShip.OnGetCoordinates -= UpdateCoordinates;
            _shipRepository.DataSpaceShip.OnGetRotation -= UpdateRotation;
            _shipRepository.DataSpaceShip.OnGetSpeed -= UpdateSpeed;

            _gameOver.OnGameOver -= GameOver;
            _gameOver.OnGameOver -= UpdateRecordScore;

            OpenRestartPanel();
        }
    }
}