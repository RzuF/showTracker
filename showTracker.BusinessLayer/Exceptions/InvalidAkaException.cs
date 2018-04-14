using System;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidAkaException : Exception
    {
        public InvalidAkaException(string msg) : base(msg)
        {

        }
    }
}
