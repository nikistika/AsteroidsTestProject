using System;
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
        
        [field:SerializeField] public int MaxLaserCount { get; private set; } = 3;
        [field:SerializeField] public int LaserCount { get; private set; } = 3;

        public Action<int> OnAddLaserCount;
        
        private void Awake()
        {
            // _gameplayUI.InstallMaxLaserCount(MaxLaserCount);
            // _gameplayUI.AddLaserCount(LaserCount);

            _waitLaserDuration = new(_laserDuration);
            _waitLaserDelay = new(_laserDelay);
        }

        public void Shot()
        {
            if (!_laserActive && LaserCount > 0)
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
            LaserCount += count;
            // _gameplayUI.AddLaserCount(count);
            OnAddLaserCount.Invoke(count);
        }

        public void RemoveLaserCount(int count)
        {
            LaserCount -= count;
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
            if (LaserCount < MaxLaserCount) StartCoroutine(ShotCooldown());
            _laserCooldownActive = false;
        }
    }
}