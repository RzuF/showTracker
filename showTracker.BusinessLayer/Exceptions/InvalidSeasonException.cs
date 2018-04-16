using System;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidSeasonException : Exception
    {
        public InvalidSeasonException(string msg) : base(msg)
        {

        } 
    }
}
