using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Addressable
{
    public abstract class BaseAssetLoader
    {
        protected readonly Dictionary<string, Component> CachedComponent = new();

        protected async Task<T> LoadAssetComponent<T>(string assetId) where T : Component
        {
            if (CachedComponent.TryGetValue(assetId, out var cachedComponent) && cachedComponent != null)
            {
                if (cachedComponent is T typedComponent)
                    return typedComponent;

                throw new InvalidCastException(
                    $"Cached component of type {cachedComponent.GetType()} cannot be cast to {typeof(T)}");
            }

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

            CachedComponent[assetId] = component;
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