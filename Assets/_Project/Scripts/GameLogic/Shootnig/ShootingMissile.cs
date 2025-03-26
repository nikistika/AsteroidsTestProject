using System;
using System.Collections;
using Factories;
using UnityEngine;

namespace Shooting
{
    public class ShootingMissile : MonoBehaviour
    {
        public Action<Missile> OnReturnMissileToPool;

        private bool _shotToggle;
        private WaitForSeconds _waitDelayShotTimes;

        [SerializeField] private MissileFactory _missileFactory;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private int _maxPoolSize;
        [SerializeField] private float _delayShotTimes = 1;

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