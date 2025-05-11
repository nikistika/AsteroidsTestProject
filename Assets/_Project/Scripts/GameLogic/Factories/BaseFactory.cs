using System.Threading.Tasks;
using Characters;
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
        protected readonly T Prefab;
        protected readonly IAssetLoader _assetLoader;

        private readonly int _defaultPoolSize;
        private readonly int _maxPoolSize;
        
        private ObjectPool<T> _pool;

        protected BaseFactory(
            ScreenSize screenSize,
            T prefab,
            PoolSizeSO poolSizeData,
            IAssetLoader assetLoader)
        {
            ScreenSize = screenSize;
            Prefab = prefab;

            _defaultPoolSize = poolSizeData.DefaultPoolSize;
            _maxPoolSize = poolSizeData.MaxPoolSize;
            _assetLoader = assetLoader;
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
                createFunc: () => ActionCreateObject().GetAwaiter().GetResult(),
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

        public UniTask<T> SpawnObject()
        {
            return UniTask.FromResult(_pool.Get());
        }

        protected abstract UniTask<T> ActionCreateObject();

        protected abstract void ActionGetObject(T obj);
    }
}