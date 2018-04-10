using System;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidPersonException : Exception
    {
        public InvalidPersonException(string msg) : base(msg)
        {
        }
    }
}
