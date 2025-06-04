using System;
using GameLogic.SaveLogic.SaveData.Time;
using SaveLogic;
using Zenject;

namespace GameLogic.SaveLogic.SaveData
{
    public class LocalLocalSaveService : IInitializable, ILocalSaveService
    {
        public event Action OnSaveDataChanged;
        
        private ILocalSave _localSavePlayerPrefs;
        
        private readonly ITimeService _timeService;
        
        public void Initialize()
        {
            _localSavePlayerPrefs = new LocalSavePlayerPrefs();
        }
        public LocalLocalSaveService(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void SetData(SaveConfig saveData)
        {
            saveData.SavingTime = _timeService.GetCurrentTime();
            _localSavePlayerPrefs.SetSaveData(saveData);
            OnSaveDataChanged?.Invoke();
        }
        
        public SaveConfig GetData()
        {
            if (_localSavePlayerPrefs.GetSaveData() != null)
            {
                return _localSavePlayerPrefs.GetSaveData();
            }
            return new SaveConfig();
        }
    }
}