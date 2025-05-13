using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoadingAssets
{
    public abstract class BaseAssetLoader
    {
        
        private readonly Dictionary<string, GameObject> _cachedObjects = new();
        
        
        //TODO: Думаю, словарь здесь некорректно работает, тк я сохраняю разные объекты с одинаковым id
        protected async Task<T> InstantiateAsset<T>(string assetId) where T : Component
        {
            var handle = Addressables.InstantiateAsync(assetId);
            _cachedObjects[assetId] = await handle.Task;

            if (_cachedObjects[assetId].TryGetComponent(out T loadingAsset) == false)
            {
                throw new NullReferenceException(
                    $"Object of type {typeof(T)} is null on attempt to load it from addressables"
                );
            }

            return loadingAsset;
        }

        protected void ReleaseInstanceAsset(string assetId)
        {
            var obj = _cachedObjects[assetId];
            if (obj == null)
                return;
            
            obj.SetActive(false);
            Addressables.ReleaseInstance(obj);
            _cachedObjects.Remove(assetId);
        }
    }
}