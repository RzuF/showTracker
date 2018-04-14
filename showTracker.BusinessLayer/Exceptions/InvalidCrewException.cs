using System;
using System.Collections.Generic;
using System.Text;

namespace showTracker.BusinessLayer.Exceptions
{
    public class InvalidCrewException : Exception
    {
        public InvalidCrewException(string msg) : base(msg)
        {

        }
    }
}
