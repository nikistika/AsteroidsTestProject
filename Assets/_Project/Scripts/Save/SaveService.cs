using System;
using _Project.Scripts.RemoteConfig;
using _Project.Scripts.Save;
using _Project.Scripts.Save.CloudSave;
using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData.Time;
using UnityEngine;

namespace GameLogic.SaveLogic.SaveData.Save
{
    public class SaveService : ISaveService
    {
        private readonly ILocalSaveService _localSaveService;
        private readonly ICloudSaveService _cloudSaveService;
        private readonly ITimeService _timeService;

        private SaveConfig _currentSaveData;

        SaveConfig ISaveService.CurrentSaveData
        {
            get => _currentSaveData;
            set => _currentSaveData = value;
        }

        public SaveConfig CurrentSaveData
        {
            get => _currentSaveData;
            private set => _currentSaveData = value;
        }
        
        public SaveService(
            ILocalSaveService localSaveService,
            ICloudSaveService cloudSaveService,
            ITimeService timeService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
            _timeService = timeService;
        }

        public async UniTask Initialize()
        {
            await GetData();
        }

        public async UniTask SaveData(SaveConfig data)
        {
            data.SavingTime = _timeService.GetCurrentTime();

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _localSaveService.SetData(data);
                Debug.LogWarning("SaveService.SaveData(): No internet connection.");
            }
            else
            {
                _localSaveService.SetData(data);
                await _cloudSaveService.SaveData(data);
                Debug.Log("SaveService.SaveData(): The Internet is available.");
            }

            CurrentSaveData = data;
        }

        public async UniTask<SaveConfig> GetData()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                CurrentSaveData = _localSaveService.GetData();
                Debug.LogWarning("SaveService.GetData(): No internet connection.");
            }
            else
            {
                SaveConfig cloudData = await _cloudSaveService.LoadData();
                SaveConfig localData = _localSaveService.GetData();

                DateTime timeCloudData = _timeService.ConvertToDateTime(cloudData.SavingTime);
                DateTime timeLocalData = _timeService.ConvertToDateTime(localData.SavingTime);

                if (timeCloudData < timeLocalData)
                {
                    CurrentSaveData = localData;
                }
                else
                {
                    CurrentSaveData = cloudData;
                }

                Debug.Log("SaveService.GetData(): The Internet is available.");
            }

            return CurrentSaveData;
        }
    }
}