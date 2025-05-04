namespace GameLogic.SaveLogic.SaveData
{
    public interface ISave
    {
        
        public string RecordScore { get; }
        
        public void SetRecordScore(SaveData saveData);
        public SaveData GetRecordScore(string key);

    }
}