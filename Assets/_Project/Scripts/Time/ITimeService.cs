using System;

namespace _Project.Scripts.Time
{
    public interface ITimeService
    {
        public string GetCurrentTime();
        public DateTime ConvertToDateTime(string time);

    }
}