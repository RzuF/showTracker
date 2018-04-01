using System.Diagnostics;
using showTracker.BusinessLayer.Interfaces;

namespace showTracker.BusinessLayer.Loggers
{
    public class STLogger : ISTLogger
    {
        public virtual void Log(object obj, bool doNotBreakLine = false)
        {
            if (doNotBreakLine)
            {
                Debug.Write(obj);
            }
            else
            {
                Debug.WriteLine(obj);
            }
        }

        public virtual void Log(string message, bool doNotBreakLine = false)
        {
            if (doNotBreakLine)
            {
                Debug.Write(message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }
    }
}
