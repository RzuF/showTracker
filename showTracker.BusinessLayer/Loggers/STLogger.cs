using System.Diagnostics;
using Newtonsoft.Json;
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

        public void LogWithSerialization(object obj, bool doNotBreakLine = false)
        {
            var serializedObject = JsonConvert.SerializeObject(obj);
            Log(serializedObject, doNotBreakLine);
        }
    }
}
