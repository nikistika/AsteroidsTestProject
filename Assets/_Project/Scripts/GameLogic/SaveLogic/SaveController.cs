using System;
using Zenject;

namespace GameLogic.SaveLogic.SaveData
{
    public class SaveController : IInitializable
    {
        public event Action OnSaveDataChanged;

        private ISave _savePlayerPrefs;

        public void Initialize()
        {
            _savePlayerPrefs = new SavePlayerPrefs();
        }

        public SaveController(SavePlayerPrefs savePlayerPrefs)
        {
            _savePlayerPrefs = savePlayerPrefs;
        }

        public void SetData(SavedData savedData)
        {
            _savePlayerPrefs.SetSaveData(savedData);
            OnSaveDataChanged?.Invoke();
        }

        public SavedData GetData()
        {
            if (_savePlayerPrefs.GetSaveData() != null)
            {
                return _savePlayerPrefs.GetSaveData();
            }

            return new SavedData();
        }
    }
}