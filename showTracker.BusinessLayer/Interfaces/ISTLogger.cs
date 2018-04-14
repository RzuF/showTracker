namespace showTracker.BusinessLayer.Interfaces
{
    public interface ISTLogger
    {
        void Log(object obj, bool doNotBreakLine = false);
        void Log(string message, bool doNotBreakLine = false);
        void LogWithSerialization(object obj, bool doNotBreakLine = false);
    }
}
