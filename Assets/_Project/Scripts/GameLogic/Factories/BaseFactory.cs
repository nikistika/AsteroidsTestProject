using UnityEngine;
using UnityEngine.Pool;

namespace Factories
{
    public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T _prefab;
        [SerializeField] private int _defaultPoolSize;
        [SerializeField] private int _maxPoolSize;

        private ObjectPool<T> _pool;

        protected void Awake()
        {
            PoolInitialization();
        }

        private void PoolInitialization()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => { return ActionCreateObject(); },
                actionOnGet: (obj) => { ActionGetObject(obj); },
                actionOnRelease: (obj) =>
                {
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    obj.gameObject.SetActive(false);
                },
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: _defaultPoolSize,
                maxSize: _maxPoolSize
            );
        }

        public void ReturnObject(T obj)
        {
            _pool.Release(obj);
        }

        public T SpawnObject()
        {
            return _pool.Get();
        }

        protected abstract T ActionCreateObject();

        protected abstract void ActionGetObject(T obj);
    }
}