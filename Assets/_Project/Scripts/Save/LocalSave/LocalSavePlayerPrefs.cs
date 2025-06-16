using _Project.Scripts.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Save.LocalSave
{
    public class LocalSavePlayerPrefs : ISave
    {
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public UniTask SaveData(SaveData saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SaveKeys.GeneralData2.ToString(), jsonSave);
            return UniTask.CompletedTask;
        }

        public UniTask<SaveData> GetData()
        {
            string jsonSave = PlayerPrefs.GetString(SaveKeys.GeneralData2.ToString());
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonSave);
            return UniTask.FromResult(saveData);
        }
    }
}