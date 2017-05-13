using System;
using System.Collections.Generic;
using System.Text;

namespace WordHunt.Base.Services
{
    public interface ITimeProvider
    {
        DateTime GetCurrentTime();
    }

    public class TimeProvider : ITimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}
