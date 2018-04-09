using System;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidEpisodeException : Exception
    {
        public InvalidEpisodeException(string msg) : base(msg)
        {
        }
    }
}
