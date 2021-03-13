using System;

namespace Arcturus.Application
{
    public class InvalidReferenceException : Exception
    {
        public InvalidReferenceException(object id) : base("Invalid reference")
        {

        }
    }
}
