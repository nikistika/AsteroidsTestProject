using System.Collections;
using UnityEngine;

namespace GameLogic
{
    public class ShootingLaser : MonoBehaviour
    {
        
        private bool _laserActive;
        private bool _laserCooldownActive;
        
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private GameObject _laserObject;
        [SerializeField] private float _laserTime = 1;
        [SerializeField] private float _laserDelay = 1;
        [SerializeField] private float _laserCooldown = 15;
        [SerializeField] private int _maxLaserCount = 3;
        [SerializeField] private int _laserCount = 3;

        private void Awake()
        {
            _gameplayUI.InstallMaxLaserCount(_maxLaserCount);
            _gameplayUI.AddLaserCount(_laserCount);

        }

        public void Shot()
        {
            if (!_laserActive && _laserCount > 0)
            {
                RemoveLaserCount(1);
                if (_laserCooldownActive == false) StartCoroutine(ShotCooldown());
                _laserActive = true;
                _laserObject.gameObject.SetActive(true);
                StartCoroutine(ShotDuration());
            }
        }

        private IEnumerator ShotDuration()
        {
            yield return new WaitForSeconds(_laserTime);
            _laserObject.gameObject.SetActive(false);
            yield return new WaitForSeconds(_laserDelay);
            _laserActive = false;
        }        
        
        private IEnumerator ShotCooldown()
        {
            Debug.Log("timerStart");

            _laserCooldownActive = true;
            for (float timer = _laserCooldown; timer > 0; timer--)
            {
                _gameplayUI.LaserCooldown(timer ,_laserCooldown);
                yield return new WaitForSeconds(1);
            }
            AddLaserCount(1);
            if (_laserCount < _maxLaserCount) StartCoroutine(ShotCooldown());
            _laserCooldownActive = false;
        }
        
        public void AddLaserCount(int count)
        {
            _laserCount += count;
            _gameplayUI.AddLaserCount(count);

        }

        public void RemoveLaserCount(int count)
        {
            _laserCount-= count;
            _gameplayUI.RemoveLaserCount(count);
        }
        
    }
}