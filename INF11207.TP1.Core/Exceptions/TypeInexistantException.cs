namespace INF11207.TP1.Core.Exceptions;

public class TypeInexistantException : Exception
{
    public TypeInexistantException()
    {
    }

    public TypeInexistantException(string message)
        : base(message)
    {
    }

    public TypeInexistantException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
