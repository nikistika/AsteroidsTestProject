using UnityEngine;

namespace GameLogic.SaveLogic.SaveData
{
    public class SavePlayerPrefs : ISave
    {

        public string RecordScore { get; } = "RecordScore";
        
        public void SetRecordScore(SaveData saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(RecordScore, jsonSave);
        }

        public SaveData GetRecordScore(string key)
        {
            string jsonSave = PlayerPrefs.GetString(key);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonSave);
            return saveData;
        }
    }
}