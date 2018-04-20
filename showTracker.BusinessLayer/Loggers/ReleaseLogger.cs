using showTracker.BusinessLayer.Interfaces;

namespace showTracker.BusinessLayer.Loggers
{
    public class ReleaseLogger : ISTLogger
    {
        public void Log(object obj, bool doNotBreakLine = false)
        {
        }

        public void Log(string message, bool doNotBreakLine = false)
        {
        }

        public void LogWithSerialization(object obj, bool doNotBreakLine = false)
        {
        }
    }
}