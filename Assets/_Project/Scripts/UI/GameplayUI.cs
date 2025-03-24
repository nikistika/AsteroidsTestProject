using GameLogic;
using Shooting;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayUI : MonoBehaviour
    {
        public int CurrentScore { get; private set; }
        private int _laserCount;

        private int _maxLaserCount;

        // private bool _flagMaxLaserCount;
        private string _laserCountText;

        [SerializeField] private ShootingLaser _shootingLaser;
        [SerializeField] private DataSpaceShip _dataSpaceShip;
        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;

        private void Awake()
        {
            _shootingLaser.OnAddLaserCount += AddLaserCount;
        }
        
        private void Start()
        {
            AddScore(0);
            _laserCount = InstallMaxLaserCount();
        }

        private void Update()
        {
            DisplayDataAboutCharacter();
        }

        private int InstallMaxLaserCount()
        {
            return _maxLaserCount = _shootingLaser.MaxLaserCount;
            
        }

        public void AddScore(int score)
        {
            CurrentScore += score;
            _scoreTMP.text = $"Score: {CurrentScore}";
        }

        public void AddLaserCount(int count)
        {
            _laserCount += count;
            _laserCountText = $"Laser: {_laserCount}/{_maxLaserCount}";
            _laserCountTMP.text = _laserCountText;
        }

        public void RemoveLaserCount(int count)
        {
            _laserCount -= count;
            _laserCountText = $"Laser: {_laserCount}/{_maxLaserCount}";
            _laserCountTMP.text = _laserCountText;
        }

        public void LaserCooldown(float currentCooldown, float maxCooldown)
        {
            _laserCountTMP.text = $"{_laserCountText} ({currentCooldown}/{maxCooldown})";
        }

        public void DisplayDataAboutCharacter()
        {
            _coordinatesTMP.text = $"Coordinates: {_dataSpaceShip.GetCoordinates()}";
            _rotationTMP.text = $"Rotation: {_dataSpaceShip.GetRotation()}";
            _speedTMP.text = $"Speed: {_dataSpaceShip.GetSpeed()}";
        }
    }
}