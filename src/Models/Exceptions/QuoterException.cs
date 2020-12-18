using System;

namespace Models.Exceptions
{

    public class QuoterException : Exception
    {
        public QuoterException(string errorReason) : base(errorReason)
        {
        }
    }
}