using UnityEngine;
using UnityEngine.Pool;

namespace Factories
{
    public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected float _halfHeightCamera;
        protected float _halfWidthCamera;
        protected Camera _camera;
        private ObjectPool<T> _pool;

        [SerializeField] protected T _prefab;
        [SerializeField] private int _defaultPoolSize;
        [SerializeField] private int _maxPoolSize;

        protected void Construct(Camera camera, float halfHeightCamera, float halfWidthCamera)
        {
            _camera = camera;
            _halfHeightCamera = halfHeightCamera;
            _halfWidthCamera = halfWidthCamera;
        }
        
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
                    ActionReleaseObject(obj);
                },
                actionOnDestroy: (obj) => Destroy(obj),
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