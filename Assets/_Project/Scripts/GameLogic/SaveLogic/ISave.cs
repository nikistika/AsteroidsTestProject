namespace GameLogic.SaveLogic.SaveData
{
    public interface ISave
    {
        public void SetSaveData(SavedData savedData);
        public SavedData GetSaveData();
    }
}