using _Project.Scripts.Addressable;
using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.GameLogic.Factories
{
    public abstract class BaseFactory<T> where T : MonoBehaviour
    {
        protected readonly ScreenSize ScreenSize;
        protected readonly IAssetLoader AssetLoader;
        protected readonly IRemoteConfigService RemoteConfigService;

        protected int DefaultPoolSize;
        protected int MaxPoolSize;

        protected T Prefab;
        private ObjectPool<T> _pool;

        protected BaseFactory(
            ScreenSize screenSize,
            IAssetLoader assetLoader,
            IRemoteConfigService remoteConfigService)
        {
            ScreenSize = screenSize;
            AssetLoader = assetLoader;
            RemoteConfigService = remoteConfigService;
        }

        public async UniTask StartWork()
        {
            if (_pool == null)
            {
                InitializeFactory();
                await GetPrefab();
                PoolInitialize();
            }
        }

        public void ReturnObject(T obj)
        {
            _pool.Release(obj);
        }

        public T SpawnObject()
        {
            return _pool.Get();
        }

        protected abstract void InitializeFactory();

        protected abstract void ActionReleaseObject(T obj);

        protected abstract T ActionCreateObject();

        protected abstract void ActionGetObject(T obj);

        protected abstract UniTask GetPrefab();

        private void PoolInitialize()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => { return ActionCreateObject(); },
                actionOnGet: (obj) => { ActionGetObject(obj); },
                actionOnRelease: (obj) => { ActionReleaseObject(obj); },
                actionOnDestroy: (obj) => { Object.Destroy(obj); },
                collectionCheck: false,
                defaultCapacity: DefaultPoolSize,
                maxSize: MaxPoolSize
            );
        }
    }
}