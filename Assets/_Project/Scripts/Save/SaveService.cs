using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Time;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Save
{
    public class SaveService : ISaveService
    {
        public event Action OnSaveDataChanged;

        private readonly ISave _localSaveService;
        private readonly ISave _cloudSaveService;
        private readonly ITimeService _timeService;

        private SaveData _currentSaveData;

        SaveData ISaveService.CurrentSaveData
        {
            get => _currentSaveData;
            set => _currentSaveData = value;
        }

        public SaveData CurrentSaveData
        {
            get => _currentSaveData;
            private set => _currentSaveData = value;
        }

        public SaveService(
            [Inject(Id = GlobalInstallerIDs.SaveLocalService)]
            ISave localSaveService,
            [Inject(Id = GlobalInstallerIDs.SaveCloudService)]
            ISave cloudSaveService,
            ITimeService timeService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
            _timeService = timeService;
        }

        public async UniTask Initialize()
        {
            await _localSaveService.Initialize();
            await _cloudSaveService.Initialize();
            await GetData();
        }

        public async UniTask SaveData(SaveData data)
        {
            data.SavingTime = _timeService.GetCurrentTime();

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _localSaveService.SaveData(data);
                Debug.LogWarning("SaveService.SaveData(): No internet connection.");
            }
            else
            {
                _localSaveService.SaveData(data);
                await _cloudSaveService.SaveData(data);
                Debug.Log("SaveService.SaveData(): The Internet is available.");
            }

            CurrentSaveData = data;
            OnSaveDataChanged?.Invoke();
        }

        public async UniTask<SaveData> GetData()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                CurrentSaveData = await _localSaveService.GetData();
                Debug.LogWarning("SaveService.GetData(): No internet connection.");
            }
            else
            {
                SaveData cloudData = await _cloudSaveService.GetData();
                SaveData localData = await _localSaveService.GetData();

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