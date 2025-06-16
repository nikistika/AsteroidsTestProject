using _Project.Scripts.Addressable;
using _Project.Scripts.Ads;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.GameLogic.Shootnig;
using _Project.Scripts.Save;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.GameScene
{
    public class GameplayUIPresenter
    {
        private readonly GameplayUIView _gameplayUIView;
        private readonly GameState _gameState;
        private readonly ShipRepository _shipRepository;
        private readonly IScoreService _scoreService;
        private readonly ISaveService _saveService;
        private readonly IAdsService _adsService;
        private readonly ISceneService _sceneService;
        private readonly IAssetLoader _assetLoader;

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
            IScoreService scoreService,
            ISaveService saveService,
            IAdsService adsService,
            ISceneService sceneService,
            IAssetLoader assetLoader)
        {
            _gameplayUIView = gameplayUIView;
            _gameState = gameState;
            _shipRepository = shipRepository;
            _scoreService = scoreService;
            _saveService = saveService;
            _adsService = adsService;
            _sceneService = sceneService;
            _assetLoader = assetLoader;
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
            _gameplayUIView.OnMenuClicked += ReturnToMenu;

            _gameState.OnGameOver += OpenRestartPanel;
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
            if (_currentScore > _saveService.CurrentSaveData.ScoreRecord)
            {
                _recordScore = _currentScore;
            }
            else
            {
                _recordScore = _saveService.CurrentSaveData.ScoreRecord;
            }

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
            if (!_saveService.CurrentSaveData.AdsRemoved)
            {
                bool result = await _adsService.ShowRewardedAds();
                if (result)
                {
                    _gameplayUIView.CloseRestartPanel();
                    _gameState.ContinueGame();
                }
            }
        }

        private void ReturnToMenu()
        {
            _gameState.GameExit();
            
            _assetLoader.DestroyGameplayUIView();
            _assetLoader.DestroySpaceShip();
            _assetLoader.DestroyAsteroid();
            _assetLoader.DestroyUFO();
            _assetLoader.DestroyMissile();
            _assetLoader.DestroyAudioSources();
            
            _sceneService.GoToMenu();
        }

        private async UniTask RestartGame()
        {
            if (!_saveService.CurrentSaveData.AdsRemoved)
            {
                await _adsService.ShowInterstitialAd();
            }

            _gameState.GameExit();
            _sceneService.RestartScene();
        }

        private void OpenRestartPanel()
        {
            _gameplayUIView.SetCurrentScore($"Score {_currentScore}");
            _gameplayUIView.SetRecordScore($"Record score: {_recordScore}");

            if (_saveService.CurrentSaveData.AdsRemoved)
            {
                _gameplayUIView.HideContinueButton();
            }

            _gameplayUIView.OpenRestartPanel();
        }

        private void GameExit()
        {
            _scoreService.OnScoreChanged -= UpdateCurrentScore;
            _shootingLaser.OnEditLaserCount -= UpdateCurrentLaserCount;
            _shootingLaser.OnLaserCooldown -= UpdateCurrentLaserTime;

            _shipRepository.DataSpaceShip.OnGetCoordinates -= UpdateCoordinates;
            _shipRepository.DataSpaceShip.OnGetRotation -= UpdateRotation;
            _shipRepository.DataSpaceShip.OnGetSpeed -= UpdateSpeed;

            _gameState.OnGameOver -= OpenRestartPanel;
            _gameState.OnGameOver -= UpdateRecordScore;
        }
    }
}