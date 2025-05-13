using System;
using System.Collections;
using GameLogic.Analytics;
using Managers;
using UnityEngine;

namespace Shooting
{
    public class ShootingLaser : MonoBehaviour
    {
        public event Action<int> OnEditLaserCount;
        public event Action<int, int> OnLaserCooldown;

        private bool _laserActive;
        private bool _laserCooldownActive;
        private WaitForSeconds _waitLaserDuration;
        private WaitForSeconds _waitLaserDelay;
        private WaitForSeconds _waitLaserCooldown = new(1);

        private AnalyticsController _analyticsController;
        private KillService _killService;

        [SerializeField] private Laser _laserObject;
        [SerializeField] private float _laserDuration = 1;
        [SerializeField] private float _laserDelay = 1;
        [SerializeField] private int _laserCooldown = 15;

        [field: SerializeField] public int MaxLaserCount { get; private set; } = 3;
        [field: SerializeField] public int LaserCount { get; private set; }

        public void Construct(
            AnalyticsController analyticsController,
            KillService killService)
        {
            _analyticsController = analyticsController;
            _killService = killService;
        }

        private void Awake()
        {
            _waitLaserDuration = new WaitForSeconds(_laserDuration);
            _waitLaserDelay = new WaitForSeconds(_laserDelay);
            LaserCount = MaxLaserCount;
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

                UsingLaserEvent();
                _killService.AddLaser(1);
            }
        }

        private void UsingLaserEvent()
        {
            _analyticsController.UsingLaserEvent();
        }

        private void AddLaserCount(int count)
        {
            LaserCount += count;
            OnEditLaserCount?.Invoke(LaserCount);
        }

        private void RemoveLaserCount(int count)
        {
            LaserCount -= count;
            OnEditLaserCount?.Invoke(LaserCount);
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
            for (int timer = _laserCooldown; timer > 0; timer--)
            {
                OnLaserCooldown?.Invoke(timer, _laserCooldown);
                yield return _waitLaserCooldown;
            }

            AddLaserCount(1);

            if (LaserCount < MaxLaserCount)
            {
                StartCoroutine(ShotCooldown());
            }

            _laserCooldownActive = false;
        }
    }
}