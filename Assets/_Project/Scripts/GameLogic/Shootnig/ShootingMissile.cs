using System;
using System.Collections;
using Factories;
using Managers;
using UnityEngine;
using Zenject;

namespace Shooting
{
    public class ShootingMissile : MonoBehaviour
    {
        public event Action<Missile> OnReturnMissileToPool;

        private bool _shotToggle;
        private WaitForSeconds _waitDelayShotTimes;
        private MissileFactory _missileFactory;
        private KillService _killService;
        
        [SerializeField] private float _delayShotTimes = 1;

        public void Construct(
        MissileFactory missileFactory,
        KillService killService)
        {
            _missileFactory = missileFactory;
            _killService = killService;
        }

        public void Initialize()
        {
            _waitDelayShotTimes = new WaitForSeconds(_delayShotTimes);
            OnReturnMissileToPool += ReturnMissileToPool;
        }

        public async void Shot()
        {
            if (!_shotToggle)
            {
                StartCoroutine(DelayShot());
                var missile = await _missileFactory.SpawnObject();
                if (missile != null)
                {
                    _killService.AddMissile(1);
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