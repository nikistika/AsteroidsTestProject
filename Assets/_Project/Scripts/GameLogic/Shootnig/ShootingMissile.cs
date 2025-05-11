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
        private KillManager _killManager;
        
        [SerializeField] private float _delayShotTimes = 1;

        [Inject]
        public void Construct(
        MissileFactory missileFactory,
        KillManager killManager)
        {
            _missileFactory = missileFactory;
            _killManager = killManager;
        }

        private void Awake()
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
                    _killManager.AddMissile(1);
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