using System.Collections;
using UI;
using UnityEngine;

namespace Shooting
{
    public class ShootingLaser : MonoBehaviour
    {
        private bool _laserActive;
        private bool _laserCooldownActive;
        private WaitForSeconds _waitLaserDuration;
        private WaitForSeconds _waitLaserDelay;
        private WaitForSeconds _waitLaserCooldown = new (1);
        
        
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private Laser _laserObject;
        [SerializeField] private float _laserDuration = 1;
        [SerializeField] private float _laserDelay = 1;
        [SerializeField] private float _laserCooldown = 15;
        [SerializeField] private int _maxLaserCount = 3;
        [SerializeField] private int _laserCount = 3;

        private void Awake()
        {
            _gameplayUI.InstallMaxLaserCount(_maxLaserCount);
            _gameplayUI.AddLaserCount(_laserCount);

            _waitLaserDuration = new(_laserDuration);
            _waitLaserDelay = new(_laserDelay);
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

        public void AddLaserCount(int count)
        {
            _laserCount += count;
            _gameplayUI.AddLaserCount(count);
        }

        public void RemoveLaserCount(int count)
        {
            _laserCount -= count;
            _gameplayUI.RemoveLaserCount(count);
        }

        private IEnumerator ShotDuration()
        {
            yield return _waitLaserDuration;
            _laserObject.gameObject.SetActive(false);
            yield return _waitLaserDelay;
            _laserActive = false;
        }

        private IEnumerator ShotCooldown()
        {
            _laserCooldownActive = true;
            for (float timer = _laserCooldown; timer > 0; timer--)
            {
                _gameplayUI.LaserCooldown(timer, _laserCooldown);
                yield return _waitLaserCooldown;
            }

            AddLaserCount(1);
            if (_laserCount < _maxLaserCount) StartCoroutine(ShotCooldown());
            _laserCooldownActive = false;
        }
    }
}