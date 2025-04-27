using GameLogic;
using Managers;
using Player;
using Shooting;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayUI : MonoBehaviour
    {
        private int _maxLaserCount;
        private string _laserCountText;
        private ShootingLaser _shootingLaser;
        private ShipRepository _shipRepository;
        private GameOver _gameOver;
        private ScoreManager _scoreManager;

        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;

        public void Construct(ShipRepository shipRepository, GameOver gameOver, ScoreManager scoreManager)
        {
            _shipRepository = shipRepository;
            _gameOver = gameOver;
            _scoreManager = scoreManager;
        }
        
        private void Start()
        {
            _shootingLaser = _shipRepository.ShootingLaser;
            
            _shootingLaser.OnEditLaserCount += EditLaserCount;
            _shootingLaser.OnLaserCooldown += LaserCooldown;
            _scoreManager.OnScoreChanged += AddScore;
            _gameOver.OnGameOver += GameOver;
            
            AddScore(0);
            
            _maxLaserCount = _shootingLaser.MaxLaserCount;
            EditLaserCount(_shootingLaser.LaserCount);
        }



        private void Update()
        {
            if (_shipRepository.DataSpaceShip != null) DisplayDataAboutCharacter();
        }

        private void AddScore(int score)
        {
            _scoreTMP.text = $"Score: {score}";
        }

        private void EditLaserCount(int laserCount)
        {
            _laserCountText = $"Laser: {laserCount}/{_maxLaserCount}";
            _laserCountTMP.text = _laserCountText;
        }

        private void LaserCooldown(float currentCooldown, float maxCooldown)
        {
            _laserCountTMP.text = $"{_laserCountText} ({currentCooldown}/{maxCooldown})";
        }

        private void DisplayDataAboutCharacter()
        {
            _coordinatesTMP.text = $"Coordinates: {_shipRepository.DataSpaceShip.GetCoordinates()}";
            _rotationTMP.text = $"Rotation: {_shipRepository.DataSpaceShip.GetRotation()}";
            _speedTMP.text = $"Speed: {_shipRepository.DataSpaceShip.GetSpeed()}";
        }

        private void GameOver()
        {
            _shootingLaser.OnEditLaserCount -= EditLaserCount;
            _shootingLaser.OnLaserCooldown -= LaserCooldown;
            _scoreManager.OnScoreChanged -= AddScore;
            _gameOver.OnGameOver -= GameOver;
        }
    }
}