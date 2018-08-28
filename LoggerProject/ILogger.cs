using System;
using System.Diagnostics;

namespace LoggerProject
{
    public interface ILogger
    {
        void Log(Exception ex, EventLogEntryType type = EventLogEntryType.Error);
        void Log(string info);
    }
}
