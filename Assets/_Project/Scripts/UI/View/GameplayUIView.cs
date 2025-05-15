using TMPro;
using UnityEngine;

namespace UI.View
{
    public class GameplayUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScoreGameTMP;
        [SerializeField] private TMP_Text _currentScoreRestartTMP;
        [SerializeField] private TMP_Text _recordScoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;
        [SerializeField] private RestartPanel _restartPanel;

        public void SetCurrentScore(string score)
        {
            _currentScoreGameTMP.text = score;
            _currentScoreRestartTMP.text = score;
        }

        public void SetRecordScore(string score)
        {
            _recordScoreTMP.text = score;
        }

        public void SetLaserCount(string laserCountText)
        {
            _laserCountTMP.text = laserCountText;
        }

        public void SetCoordinates(string coordinatesText)
        {
            _coordinatesTMP.text = coordinatesText;
        }

        public void SetRotation(string rotationText)
        {
            _rotationTMP.text = rotationText;
        }

        public void SetSpeed(string speedText)
        {
            _speedTMP.text = speedText;
        }

        public void OpenRestartPanel()
        {
            _restartPanel.gameObject.SetActive(true);
        }
    }
}