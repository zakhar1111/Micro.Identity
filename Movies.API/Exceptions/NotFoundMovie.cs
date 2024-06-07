using System.Runtime.Serialization;

namespace Movies.API.Exceptions;

[Serializable]
internal class NotFoundMovie : Exception
{
    private Type type;

    public NotFoundMovie()
    {
    }

    public NotFoundMovie(Type type)
    {
        this.type = type;
    }

    public NotFoundMovie(string? message) : base(message)
    {
    }

    public NotFoundMovie(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotFoundMovie(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}