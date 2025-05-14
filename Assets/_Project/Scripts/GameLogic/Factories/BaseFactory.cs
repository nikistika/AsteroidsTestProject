using Cysharp.Threading.Tasks;
using GameLogic;
using LoadingAssets;
using SciptableObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Factories
{
    public abstract class BaseFactory<T> where T : MonoBehaviour
    {
        protected readonly ScreenSize ScreenSize;
        protected readonly IAssetLoader _assetLoader;

        private readonly int _defaultPoolSize;
        private readonly int _maxPoolSize;

        protected T Prefab;
        private ObjectPool<T> _pool;

        protected BaseFactory(
            ScreenSize screenSize,
            PoolSizeSO poolSizeData,
            IAssetLoader assetLoader)
        {
            ScreenSize = screenSize;
            _defaultPoolSize = poolSizeData.DefaultPoolSize;
            _maxPoolSize = poolSizeData.MaxPoolSize;
            _assetLoader = assetLoader;
        }

        public async UniTask StartWork()
        {
            if (_pool == null)
            {
                await GetPrefab();
                PoolInitialize();
            }
        }

        private void PoolInitialize()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => { return ActionCreateObject(); },
                actionOnGet: (obj) => { ActionGetObject(obj); },
                actionOnRelease: (obj) => { ActionReleaseObject(obj); },
                actionOnDestroy: (obj) => { Object.Destroy(obj); },
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

        protected abstract UniTask GetPrefab();
    }
}