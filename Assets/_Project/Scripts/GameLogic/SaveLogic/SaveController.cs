namespace GameLogic.SaveLogic.SaveData
{
    public class SaveController
    {
        private SavePlayerPrefs _savePlayerPrefs;

        public SaveController(SavePlayerPrefs savePlayerPrefs)
        {
            _savePlayerPrefs = savePlayerPrefs;
        }
        
        public void SaveRecordInPlayerPrefs(SaveData saveData)
        {
            _savePlayerPrefs.SetRecordScore(saveData);
        }
        
        public SaveData GetRecordInPlayerPrefs()
        {
            if (_savePlayerPrefs.GetRecordScore(_savePlayerPrefs.RecordScore) != null)
            {
                return _savePlayerPrefs.GetRecordScore(_savePlayerPrefs.RecordScore);
            }
            return new SaveData();
        }
        
    }
}