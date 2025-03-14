using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayUI : MonoBehaviour
    {
        public int CurrentScore { get; private set; }
        private int _laserCount;
        private int _maxLaserCount;
        private bool _flagMaxLaserCount;
        private string _laserCountText;

        [SerializeField] private TMP_Text _scoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;

        private void Start()
        {
            AddScore(0);
        }

        public void InstallMaxLaserCount(int maxLaserCount)
        {
            if (!_flagMaxLaserCount)
            {
                _maxLaserCount = maxLaserCount;
                _flagMaxLaserCount = true;
            }
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

        public void DisplayDataAboutCharacter(string coordinates, string rotation, string speed)
        {
            _coordinatesTMP.text = $"Coordinates: {coordinates}";
            _rotationTMP.text = $"Rotation: {rotation}";
            _speedTMP.text = $"Speed: {speed}";
        }
    }
}