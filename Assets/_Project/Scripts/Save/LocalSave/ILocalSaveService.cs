using System;

namespace GameLogic.SaveLogic.SaveData
{
    public interface ILocalSaveService
    {
        public event Action OnSaveDataChanged;
        public void SetData(SaveConfig saveData);
        public SaveConfig GetData();
    }
}