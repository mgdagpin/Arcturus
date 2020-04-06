using System;

namespace Canary.Application
{
    public class InvalidReferenceException : Exception
    {
        public InvalidReferenceException(object id) : base("Invalid reference")
        {

        }
    }
}
