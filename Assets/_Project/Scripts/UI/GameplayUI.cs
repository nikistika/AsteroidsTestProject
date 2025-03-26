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

        [SerializeField] private ShootingLaser _shootingLaser;
        [SerializeField] private DataSpaceShip _dataSpaceShip;
        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;

        private void Awake()
        {
            _shootingLaser.OnEditLaserCount += EditLaserCount;
            _shootingLaser.OnLaserCooldown += LaserCooldown;
            _dataSpaceShip.OnScoreChanged += AddScore;
        }

        private void Start()
        {
            AddScore(0);
            _maxLaserCount = _shootingLaser.MaxLaserCount;
            EditLaserCount(_shootingLaser.LaserCount);
        }

        private void Update()
        {
            DisplayDataAboutCharacter();
        }

        public void AddScore(int score)
        {
            _scoreTMP.text = $"Score: {score}";
        }

        public void EditLaserCount(int laserCount)
        {
            _laserCountText = $"Laser: {laserCount}/{_maxLaserCount}";
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