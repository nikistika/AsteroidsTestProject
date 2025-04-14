using UnityEngine;
using UnityEngine.Pool;

namespace Factories
{
    public abstract class BaseFactory<T> where T : MonoBehaviour
    {
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        protected Camera _camera;
        private ObjectPool<T> _pool;
        
        protected T _prefab;
        
        //TODO: Это в скриптбл обжект 
        private int _defaultPoolSize = 10;
        private int _maxPoolSize = 30;

        public BaseFactory(Camera camera, float halfHeightCamera, float halfWidthCamera, T prefab)
        {
            _camera = camera;
            _halfHeightCamera = halfHeightCamera;
            _halfWidthCamera = halfWidthCamera;
            _prefab = prefab;
        }

        public void StartWork()
        {
            if (_pool == null)
            {
                PoolInitialization();
            }
        }

        private void PoolInitialization()
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