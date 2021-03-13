using System;

namespace Arcturus.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception InnermostException(this Exception exception)
        {
            if (exception.InnerException != null)
            {
                return InnermostException(exception.InnerException);
            }

            return exception;
        }
    }
}