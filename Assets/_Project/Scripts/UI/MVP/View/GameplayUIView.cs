using TMPro;
using UnityEngine;

namespace UI.View
{
    public class GameplayUIView : MonoBehaviour
    {
        private TMP_Text _scoreTMP;
        private TMP_Text _laserCountTMP;
        private TMP_Text _coordinatesTMP;
        private TMP_Text _rotationTMP;
        private TMP_Text _speedTMP;
        
        public void SetScore(string score)
        {
            _scoreTMP.text = score;
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
    }
}