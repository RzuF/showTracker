using System;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidCastException : Exception
    {
        public InvalidCastException(string msg) : base(msg)
        {

        }
    }
}
