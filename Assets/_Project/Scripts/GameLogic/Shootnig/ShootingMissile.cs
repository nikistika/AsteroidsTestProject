using System;
using System.Collections;
using _Project.Scripts.AnimationControllers;
using _Project.Scripts.Audio;
using _Project.Scripts.GameLogic.Factories;
using _Project.Scripts.GameLogic.Services;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Shootnig
{
    public class ShootingMissile : MonoBehaviour
    {
        public event Action<Missile> OnReturnMissileToPool;

        private bool _shotToggle;
        private WaitForSeconds _waitDelayShotTimes;
        private MissileFactory _missileFactory;
        private IKillService _killService;
        private IAudioService _audioService;
        private ShootingAnimationController _shootingAnimationController;

        [SerializeField] private float _delayShotTimes = 1;

        public void Construct(
        MissileFactory missileFactory,
        IKillService killService,
        IAudioService audioService,
        ShootingAnimationController shootingAnimationController)
        {
            _missileFactory = missileFactory;
            _killService = killService;
            _audioService = audioService;
            _shootingAnimationController = shootingAnimationController;
        }

        public void Initialize()
        {
            _waitDelayShotTimes = new WaitForSeconds(_delayShotTimes);
            OnReturnMissileToPool += ReturnMissileToPool;
        }

        public void Shot()
        {
            if (!_shotToggle)
            {
                StartCoroutine(DelayShot());
                var missile = _missileFactory.SpawnObject();
                if (missile != null)
                {
                    _audioService.PlayMissileShotAudio();
                    _killService.AddMissile(1);
                     _shootingAnimationController.ActivateShooting();
                }
            }
        }

        public void InvokeOnReturnMissileToPool(Missile missile)
        {
            if (OnReturnMissileToPool != null)
            {
                OnReturnMissileToPool?.Invoke(missile);
            }
        }

        private void ReturnMissileToPool(Missile missile)
        {
            _missileFactory.ReturnObject(missile);
        }

        private IEnumerator DelayShot()
        {
            _shotToggle = true;
            yield return _waitDelayShotTimes;
            _shotToggle = false;
        }
    }
}