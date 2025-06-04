using System;
using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData.Time;
using SaveLogic;
using UnityEngine;
using Zenject;

namespace GameLogic.SaveLogic.SaveData.Save
{
    public class SaveService : IInitializable ,ISaveService
    {
        private readonly ILocalSaveService _localSaveService;
        private readonly ICloudSaveService _cloudSaveService;
        private readonly ITimeService _timeService;

        public SaveConfig CurrentSaveData { get; private set; }
        
        public SaveService(
            ILocalSaveService localSaveService,
            ICloudSaveService cloudSaveService,
            ITimeService timeService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
            _timeService = timeService;
        }
        
        public async void Initialize()
        {
            await GetData();
        }

        public async UniTask SaveData(SaveConfig data)
        {
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