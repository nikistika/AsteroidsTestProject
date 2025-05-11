using Zenject;

namespace GameLogic.SaveLogic.SaveData
{
    public class SaveController : IInitializable
    {
        private ISave _savePlayerPrefs;
        
        public void Initialize()
        {
            _savePlayerPrefs = new SavePlayerPrefs();
        }
        public SaveController(SavePlayerPrefs savePlayerPrefs)
        {
            _savePlayerPrefs = savePlayerPrefs;
        }
        
        public void SaveRecord(SaveData saveData)
        {
            _savePlayerPrefs.SetSaveData(saveData);
        }
        
        public SaveData GetRecord()
        {
            if (_savePlayerPrefs.GetSaveData() != null)
            {
                return _savePlayerPrefs.GetSaveData();
            }
            return new SaveData();
        }
    }
}