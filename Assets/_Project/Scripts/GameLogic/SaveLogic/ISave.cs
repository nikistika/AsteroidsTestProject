namespace GameLogic.SaveLogic.SaveData
{
    public interface ISave
    {
        public void SetSaveData(SaveData saveData);
        public SaveData GetSaveData();

    }
}