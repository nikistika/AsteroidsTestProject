using System;

namespace GameLogic.SaveLogic.SaveData.Time
{
    public interface ITimeService
    {
        public string GetCurrentTime();
        public DateTime ConvertToDateTime(string time);

    }
}