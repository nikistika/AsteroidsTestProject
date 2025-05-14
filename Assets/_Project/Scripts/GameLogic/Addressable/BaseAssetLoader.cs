using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoadingAssets
{
    public abstract class BaseAssetLoader
    {
        protected readonly Dictionary<string, GameObject> CachedComponent = new();

        protected async Task<T> LoadPrefab<T>(string assetId) where T : Component
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetId);
            var prefab = await handle.Task;

            if (prefab == null)
            {
                throw new NullReferenceException($"Addressables.LoadAssetAsync returned null for key '{assetId}'");
            }

            if (prefab.TryGetComponent(out T component) == false)
            {
                throw new NullReferenceException(
                    $"Component of type {typeof(T)} not found on prefab '{prefab.name}'"
                );
            }

            CachedComponent[assetId] = prefab;
            return component;
        }
        
        protected void ReleasePrefab(string assetId)
        {
            var obj = CachedComponent[assetId].gameObject;
            if (obj == null)
                return;

            obj.SetActive(false);
            Addressables.ReleaseInstance(obj);
            CachedComponent.Remove(assetId);
        }
    }
}