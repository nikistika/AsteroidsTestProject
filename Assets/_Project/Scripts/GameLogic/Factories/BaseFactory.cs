using SciptableObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Factories
{
    public abstract class BaseFactory<T> where T : MonoBehaviour
    {
        protected float HalfHeightCamera;
        protected float HalfWidthCamera;
        protected T Prefab;
        
        private ObjectPool<T> _pool;
        private int _defaultPoolSize; 
        private int _maxPoolSize;

        protected BaseFactory(float halfHeightCamera, float halfWidthCamera, T prefab, PoolSizeSO poolSizeData)
        {
            HalfHeightCamera = halfHeightCamera;
            HalfWidthCamera = halfWidthCamera;
            Prefab = prefab;

            _defaultPoolSize = poolSizeData.DefaultPoolSize;
            _maxPoolSize = poolSizeData.MaxPoolSize;
        }

        public void StartWork()
        {
            if (_pool == null)
            {
                PoolInitialize();
            }
        }

        private void PoolInitialize()
        {
            _pool = new ObjectPool<T>(
                createFunc: () =>
                {
                    return ActionCreateObject();
                },
                actionOnGet: (obj) =>
                {
                    ActionGetObject(obj);
                },
                actionOnRelease: (obj) =>
                {
                    ActionReleaseObject(obj);
                },
                actionOnDestroy: (obj) =>
                {
                    Object.Destroy(obj);
                },
                collectionCheck: false,
                defaultCapacity: _defaultPoolSize,
                maxSize: _maxPoolSize
            );
        }

        protected abstract void ActionReleaseObject(T obj);

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