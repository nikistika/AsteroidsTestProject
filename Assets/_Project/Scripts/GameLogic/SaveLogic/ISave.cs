namespace GameLogic.SaveLogic.SaveData
{
    public interface ISave
    {
        public void SetRecordScore(SaveData saveData);
        public SaveData GetRecordScore(string key);

    }
}