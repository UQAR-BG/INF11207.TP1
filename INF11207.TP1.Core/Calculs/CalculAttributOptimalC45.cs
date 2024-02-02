namespace INF11207.TP1.Core.Calculs;

public class CalculAttributOptimalC45 : CalculAttributOptimal
{
    public static string Id => "CalculAttributOptimalC45";
    
    private static ICalculAvecAttribut _gain = Registry.CreateAndRegister<CalculRatioGain>(Id);
    
    public CalculAttributOptimalC45() : base(_gain)
    {
    }
}