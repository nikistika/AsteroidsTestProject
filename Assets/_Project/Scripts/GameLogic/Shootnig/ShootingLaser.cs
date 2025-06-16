using System;
using System.Collections;
using _Project.Scripts.Analytics;
using _Project.Scripts.Audio;
using _Project.Scripts.GameLogic.Services;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Shootnig
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

        private IAnalyticsService _analyticsService;
        private IKillService _killService;
        private IAudioService _audioService;

        [SerializeField] private Laser _laserObject;
        [SerializeField] private float _laserDuration = 0.3f;
        [SerializeField] private float _laserDelay = 1;
        [SerializeField] private int _laserCooldown = 5;

        [field: SerializeField] public int MaxLaserCount { get; private set; } = 3;
        [field: SerializeField] public int LaserCount { get; private set; }

        public void Construct(
            IAnalyticsService analyticsService,
            IKillService killService,
            IAudioService audioService)
        {
            _analyticsService = analyticsService;
            _killService = killService;
            _audioService = audioService;
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
                _audioService.PlayLaserShotAudio();
                _killService.AddLaser(1);
            }
        }

        private void UsingLaserEvent()
        {
            _analyticsService.UsingLaserEvent();
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