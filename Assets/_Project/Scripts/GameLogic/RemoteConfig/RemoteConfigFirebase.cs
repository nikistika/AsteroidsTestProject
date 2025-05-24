using System;
using ConfigData;
using ConfigKeys;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace GameLogic.RemoteConfig
{
    public class RemoteConfigFirebase
    {
        public async UniTask Initialize()
        {
            await CheckRemoteConfigValues();
        }

        private async UniTask CheckRemoteConfigValues()
        {
            Debug.Log("Fetching data...");
            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        }

        public async UniTask<PoolSizeData> GetPoolSizeConfig(PoolSizeKeys key)
        {
            var keyString = key.ToString();

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(GetPoolSizeConfig)} was unsuccessful\n{nameof(info.LastFetchStatus)}: " +
                               $"{info.LastFetchStatus}");
                return null;
            }

            await remoteConfig.ActivateAsync().AsUniTask();
            Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

            string jsonData = remoteConfig.GetValue(keyString).StringValue;
            var configData = JsonUtility.FromJson<PoolSizeData>(jsonData);
            return configData;
        }

        public async UniTask<SpawnData> GetSpawnConfig(SpawnKey key)
        {
            var keyString = key.ToString();

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(GetSpawnConfig)} was unsuccessful\n{nameof(info.LastFetchStatus)}: " +
                               $"{info.LastFetchStatus}");
                return null;
            }

            await remoteConfig.ActivateAsync().AsUniTask();
            Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

            string jsonData = remoteConfig.GetValue(keyString).StringValue;
            var configData = JsonUtility.FromJson<SpawnData>(jsonData);
            return configData;
        }
    }
}