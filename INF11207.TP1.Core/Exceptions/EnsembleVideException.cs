namespace INF11207.TP1.Core.Exceptions;

public class EnsembleVideException : Exception
{
    public EnsembleVideException()
    {
    }

    public EnsembleVideException(string message)
        : base(message)
    {
    }

    public EnsembleVideException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
