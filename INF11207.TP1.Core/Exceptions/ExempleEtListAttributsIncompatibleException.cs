namespace INF11207.TP1.Core.Exceptions;

public class ExempleEtListAttributsIncompatibleException : Exception
{
    public ExempleEtListAttributsIncompatibleException()
    {
    }

    public ExempleEtListAttributsIncompatibleException(string message)
        : base(message)
    {
    }

    public ExempleEtListAttributsIncompatibleException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
