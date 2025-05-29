using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class GameplayUIView : MonoBehaviour
    {
        public event Func<UniTask> OnContinueClicked;
        public event Func<UniTask> OnRestartClicked;
        public event Action OnMenuClicked;
        

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        [SerializeField] private TMP_Text _currentScoreGameTMP;
        [SerializeField] private TMP_Text _currentScoreRestartTMP;
        [SerializeField] private TMP_Text _recordScoreTMP;
        [SerializeField] private TMP_Text _laserCountTMP;
        [SerializeField] private TMP_Text _coordinatesTMP;
        [SerializeField] private TMP_Text _rotationTMP;
        [SerializeField] private TMP_Text _speedTMP;
        [SerializeField] private Image _restartPanel;

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

        public void ContinueClicked()
        {
            _continueButton.interactable = false;
            OnContinueClicked?.Invoke();
        }

        public void HideContinueButton()
        {
            _continueButton.interactable = false;
        }

        public void RestartClicked()
        {
            OnRestartClicked?.Invoke();
        }
        
        public void MenuClicked()
        {
            OnMenuClicked?.Invoke();
        }

        public void OpenRestartPanel()
        {
            _restartPanel.gameObject.SetActive(true);
        }

        public void CloseRestartPanel()
        {
            _restartPanel.gameObject.SetActive(false);
        }
    }
}