using System;
using System.Collections;
using Factories;
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

        [SerializeField] private float _delayShotTimes = 1;

        [Inject]
        public void Construct(MissileFactory missileFactory)
        {
            _missileFactory = missileFactory;
        }

        private void Awake()
        {
            _waitDelayShotTimes = new WaitForSeconds(_delayShotTimes);
            OnReturnMissileToPool += ReturnMissileToPool;
        }

        public void Shot()
        {
            if (!_shotToggle)
            {
                StartCoroutine(DelayShot());
                _missileFactory.SpawnObject();
            }
        }

        public void InvokeOnReturnMissileToPool(Missile missile)
        {
            if (OnReturnMissileToPool != null)
            {
                OnReturnMissileToPool.Invoke(missile);
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