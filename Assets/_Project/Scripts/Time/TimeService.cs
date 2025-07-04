﻿using System;
using System.Globalization;

namespace _Project.Scripts.Time
{
    public class TimeService : ITimeService
    {
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("O");
        }

        public DateTime ConvertToDateTime(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                return DateTime.Now;
            }

            return DateTime.Parse(time, null, DateTimeStyles.RoundtripKind);
        }
    }
}