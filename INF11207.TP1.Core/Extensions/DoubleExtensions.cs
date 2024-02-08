namespace INF11207.TP1.Core.Extensions;

public static class DoubleExtensions
{
    private const double Tolerance = 0.0000001;
    
    public static bool Equals(this double left, double right)
    {
        return Math.Abs(left - right) < Tolerance;
    }
}