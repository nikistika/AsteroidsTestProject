using GameLogic;
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
        private DataSpaceShip _dataSpaceShip;
        
        [SerializeField] private GameOver _gameOver;
        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;

        public void Construct(ShootingLaser shootingLaser, DataSpaceShip dataSpaceShip)
        {
            _shootingLaser = shootingLaser;
            _dataSpaceShip = dataSpaceShip;
        }

        private void Start()
        {
            _shootingLaser.OnEditLaserCount += EditLaserCount;
            _shootingLaser.OnLaserCooldown += LaserCooldown;
            _dataSpaceShip.OnScoreChanged += AddScore;
            _gameOver.OnGameOver += GameOver;
            
            AddScore(0);
            _maxLaserCount = _shootingLaser.MaxLaserCount;
            EditLaserCount(_shootingLaser.LaserCount);
        }

        private void Update()
        {
            DisplayDataAboutCharacter();
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
            _coordinatesTMP.text = $"Coordinates: {_dataSpaceShip.GetCoordinates()}";
            _rotationTMP.text = $"Rotation: {_dataSpaceShip.GetRotation()}";
            _speedTMP.text = $"Speed: {_dataSpaceShip.GetSpeed()}";
        }

        private void GameOver()
        {
            _shootingLaser.OnEditLaserCount -= EditLaserCount;
            _shootingLaser.OnLaserCooldown -= LaserCooldown;
            _dataSpaceShip.OnScoreChanged -= AddScore;
            _gameOver.OnGameOver -= GameOver;

        }
    }
}