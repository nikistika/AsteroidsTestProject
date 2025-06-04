namespace GameLogic.SaveLogic.SaveData
{
    public interface ILocalSave
    {
        public void SetSaveData(SaveConfig saveConfig);
        public SaveConfig GetSaveData();

    }
}