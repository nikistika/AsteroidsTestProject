using System;
using System.Globalization;

namespace GameLogic.SaveLogic.SaveData.Time
{
    public class TimeService : ITimeService
    {
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("O");
        }

        public DateTime ConvertToDateTime(string time)
        {
            return DateTime.Parse(time, null, DateTimeStyles.RoundtripKind);
        }
    }
}