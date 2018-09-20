using System;
using System.Runtime.Serialization;

namespace GamesProject.Logic.Common.Exceptions
{
    [Serializable]
    public class GamesPageParsingException : Exception
    {
        public GamesPageParsingException()
        {
        }

        public GamesPageParsingException(string message)
            : base(message)
        {
        }

        public GamesPageParsingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected GamesPageParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
