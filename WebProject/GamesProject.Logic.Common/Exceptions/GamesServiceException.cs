using System;
using System.Runtime.Serialization;

namespace GamesProject.Logic.Common.Exceptions
{
    [Serializable]
    public class GamesServiceException : Exception
    {
        public ErrorType ErrorCode { get; }

        public GamesServiceException(ErrorType errorCodeType)
        {
            this.ErrorCode = errorCodeType;
        }

        public GamesServiceException(string message, ErrorType errorCodeType)
            : base(message)
        {
            this.ErrorCode = errorCodeType;
        }

        public GamesServiceException(string message, Exception inner
            , ErrorType errorCodeType)
            : base(message, inner)
        {
            this.ErrorCode = errorCodeType;
        }

        protected GamesServiceException(SerializationInfo info, StreamingContext context
            , ErrorType errorCodeType)
            : base(info, context)
        {
            this.ErrorCode = errorCodeType;
        }
    }

    public enum ErrorType
    {
        ValidationException,
        BadRequest
    }
}
