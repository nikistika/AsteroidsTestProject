using _Project.Scripts.Enums;
using _Project.Scripts.RemoteConfig;
using UnityEngine;

namespace _Project.Scripts.Save.LocalSave
{
    public class LocalSavePlayerPrefs : ILocalSave
    {
        public void SetSaveData(SaveConfig saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SaveKeys.GeneralData2.ToString(), jsonSave);
        }

        public SaveConfig GetSaveData()
        {
            string jsonSave = PlayerPrefs.GetString(SaveKeys.GeneralData2.ToString());
            SaveConfig saveData = JsonUtility.FromJson<SaveConfig>(jsonSave);
            return saveData;
        }
    }
}