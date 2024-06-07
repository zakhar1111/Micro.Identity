using System.Runtime.Serialization;

namespace Movies.API.Exceptions
{
    [Serializable]
    internal class BadIdMovie : Exception
    {
        private Type type;

        public BadIdMovie()
        {
        }

        public BadIdMovie(Type type)
        {
            this.type = type;
        }

        public BadIdMovie(string? message) : base(message)
        {
        }

        public BadIdMovie(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BadIdMovie(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}