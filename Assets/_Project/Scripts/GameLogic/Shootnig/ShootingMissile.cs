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
                }, // Функция создания объекта
                actionOnGet: (obj) =>
                {
                    obj.gameObject.SetActive(true);
                    obj.transform.position = transform.position;
                    obj.Move();
                }, // Действие при выдаче объекта
                actionOnRelease: (obj) => {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                }, // Действие при возврате в пул
                actionOnDestroy: (obj) => Destroy(obj), // Действие при удалении объекта
                collectionCheck: false, // Проверять ли повторное добавление объекта в пул
                defaultCapacity: _poolSize, // Начальный размер пула
                maxSize: _maxPoolSize // Максимальный размер пула
            );
        }
    }
}