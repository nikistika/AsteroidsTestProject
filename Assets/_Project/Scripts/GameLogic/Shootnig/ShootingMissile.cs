using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Shooting
{
    public class ShootingMissile : MonoBehaviour
    {
        
        private bool _shotToggle;
        private ObjectPool<Missile> _missilePool;
        private WaitForSeconds _waitDelayShotTimes;
        
        [SerializeField] private Missile _missilePrefab;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private int _maxPoolSize;
        [SerializeField] private float _delayShotTimes = 1;
        
        private void Awake()
        {
            PoolInitialization();
            _waitDelayShotTimes = new WaitForSeconds(_delayShotTimes);
        }

        public void ShotMissile()
        {
            if (!_shotToggle)
            {
                StartCoroutine(DelayShot());
                _missilePool.Get();
            }
        }
        

        private IEnumerator DelayShot()
        {
            _shotToggle = true;
            yield return _waitDelayShotTimes;
            _shotToggle = false;
        }

        private void PoolInitialization()
        {
            _missilePool = new ObjectPool<Missile>(
                createFunc: () =>
                {
                    var missile = Instantiate(_missilePrefab, gameObject.transform, true);
                    missile.transform.position = transform.position;

                    missile.Construct(_missilePool);

                    return missile;
                }, 
                actionOnGet: (obj) =>
                {
                    obj.gameObject.SetActive(true);
                    obj.transform.position = transform.position;
                    obj.Move();
                },
                actionOnRelease: (obj) => {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                },
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false, 
                defaultCapacity: _poolSize,
                maxSize: _maxPoolSize
            );
        }
    }
}