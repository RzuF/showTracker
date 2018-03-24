using System;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidShowException : Exception
    {
        public InvalidShowException(string msg) : base(msg)
        {

        }
    }
}
