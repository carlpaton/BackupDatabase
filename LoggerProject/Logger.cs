using System;
using System.Configuration;
using System.Diagnostics;

namespace LoggerProject
{
    public class Logger : ILogger
    {
        readonly string logName = ConfigurationManager.AppSettings["EventLogName"];

        public void Log(Exception ex, EventLogEntryType type = EventLogEntryType.Error)
        {
            try
            {
                if (!EventLog.SourceExists(logName))
                    EventLog.CreateEventSource(logName, logName);

                string remoteIP = "";

                // Web Only
                //try
                //{
                //    remoteIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //}
                //catch { }

                string message = string.Format("{0}\n\n{1}\n\n{2}",
                    ex.Message,
                    ex.StackTrace,
                    remoteIP);

                EventLog.WriteEntry(logName, message, type);
            }
            catch (Exception ex1)
            {
                var e = ex1.Message; //used with dev debug
            }
        }

        public void Log(string info)
        {
            try
            {
                if (!EventLog.SourceExists(logName))
                    EventLog.CreateEventSource(logName, logName);

                EventLog.WriteEntry(logName, info, EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                var e = ex.Message; //used with dev debug
            }
        }
    }
}
