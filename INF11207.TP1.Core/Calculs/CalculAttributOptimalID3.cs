namespace INF11207.TP1.Core.Calculs;

public class CalculAttributOptimalID3 : CalculAttributOptimal
{
    public static string Id => "CalculAttributOptimalID3";
    
    private static ICalculAvecAttribut _gain = Registry.CreateAndRegister<CalculGain>(Id);
    
    public CalculAttributOptimalID3() : base(_gain)
    {
    }
}