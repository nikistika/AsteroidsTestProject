using UnityEngine;

namespace GameLogic.SaveLogic.SaveData
{
    public class SavePlayerPrefs : ISave
    {
        private readonly string _recordScore = "RecordScore";

        public void SetSaveData(SaveData saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(_recordScore, jsonSave);
        }

        public SaveData GetSaveData()
        {
            string jsonSave = PlayerPrefs.GetString(_recordScore);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonSave);
            return saveData;
        }
    }
}