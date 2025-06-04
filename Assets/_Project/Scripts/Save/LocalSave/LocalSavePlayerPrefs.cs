using GameLogic.Enums;
using UnityEngine;

namespace GameLogic.SaveLogic.SaveData
{
    public class LocalSavePlayerPrefs : ILocalSave
    {
        public void SetSaveData(SaveConfig saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SaveKeys.GeneralData1.ToString(), jsonSave);
        }

        public SaveConfig GetSaveData()
        {
            string jsonSave = PlayerPrefs.GetString(SaveKeys.GeneralData1.ToString());
            SaveConfig saveData = JsonUtility.FromJson<SaveConfig>(jsonSave);
            return saveData;
        }
    }
}