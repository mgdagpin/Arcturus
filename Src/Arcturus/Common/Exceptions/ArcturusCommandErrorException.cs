using Arcturus.Common.Extensions;
using System;
using System.Runtime.Serialization;

namespace Arcturus.Common.Exceptions
{
    public class ArcturusCommandErrorException : Exception
    {
        public ArcturusCommandErrorException()
        {
        }

        public ArcturusCommandErrorException(Exception exception) : base(exception.InnermostException().Message)
        {

        }

        public ArcturusCommandErrorException(string message) : base(message)
        {
        }

        public ArcturusCommandErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArcturusCommandErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
