using UnityEngine;

namespace GameLogic.SaveLogic.SaveData
{
    public class SavePlayerPrefs : ISave
    {
        private readonly string _recordScore = "RecordScore";

        public void SetSaveData(SavedData savedData)
        {
            string jsonSave = JsonUtility.ToJson(savedData);
            PlayerPrefs.SetString(_recordScore, jsonSave);
        }

        public SavedData GetSaveData()
        {
            string jsonSave = PlayerPrefs.GetString(_recordScore);
            SavedData savedData = JsonUtility.FromJson<SavedData>(jsonSave);
            return savedData;
        }
    }
}