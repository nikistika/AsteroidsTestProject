using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Ads;
using GameLogic.SaveLogic.SaveData;
using Managers;
using Player;
using Shooting;
using UI.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Presenter
{
    public class GameplayUIPresenter
    {
        private readonly GameplayUIView _gameplayUIView;
        private readonly GameState _gameState;
        private readonly ShipRepository _shipRepository;
        private readonly ScoreService _scoreService;
        private readonly SaveController _saveController;
        private readonly AdsController _adsController;

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
            GameState gameState,
            ShipRepository shipRepository,
            ScoreService scoreService,
            SaveController saveController,
            AdsController adsController)
        {
            _gameplayUIView = gameplayUIView;
            _gameState = gameState;
            _shipRepository = shipRepository;
            _scoreService = scoreService;
            _saveController = saveController;
            _adsController = adsController;
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

            _gameplayUIView.OnContinueClicked += ContinueGame;
            _gameplayUIView.OnRestartClicked += RestartGame;

            _gameState.OnGameOver += GameState;
            _gameState.OnGameOver += UpdateRecordScore;

            _gameState.OnGameExit += GameExit;

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

        private async UniTask ContinueGame()
        {
            bool result = await _adsController.ShowRewardedAds();
            if (result)
            {
                _gameplayUIView.CloseRestartPanel();
                _gameState.ContinueGame();
            }
        }

        private async UniTask RestartGame()
        {
            await _adsController.ShowInterstitialAd();
            _gameState.OnOnGameExit();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OpenRestartPanel()
        {
            _gameplayUIView.SetCurrentScore($"Score {_currentScore}");
            _gameplayUIView.SetRecordScore($"Record score: {_recordScore}");
            _gameplayUIView.OpenRestartPanel();
        }

        private void GameState()
        {
            OpenRestartPanel();
        }

        private void GameExit()
        {
            _scoreService.OnScoreChanged -= UpdateCurrentScore;
            _shootingLaser.OnEditLaserCount -= UpdateCurrentLaserCount;
            _shootingLaser.OnLaserCooldown -= UpdateCurrentLaserTime;

            _shipRepository.DataSpaceShip.OnGetCoordinates -= UpdateCoordinates;
            _shipRepository.DataSpaceShip.OnGetRotation -= UpdateRotation;
            _shipRepository.DataSpaceShip.OnGetSpeed -= UpdateSpeed;

            _gameState.OnGameOver -= GameState;
            _gameState.OnGameOver -= UpdateRecordScore;
        }
    }
}