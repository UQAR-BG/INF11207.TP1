namespace INF11207.TP1.Core.Calculs;

public class CalculRatioGain : ICalculAvecAttribut, IIdentifiable
{
    public static string Id => "CalculRatioGain";
    
    public double Calculer(Ensemble ensemble)
    {
        return Calculer(ensemble, "");
    }

    public double Calculer(Ensemble ensemble, string attribut)
    {
        double split = Registry.CreateAndRegister<CalculSplit>(CalculSplit.Id)
            .Calculer(ensemble, attribut);
        double gain = Registry.CreateAndRegister<CalculGain>(CalculGain.Id)
            .Calculer(ensemble, attribut);

        return split != 0 ? gain / split : double.PositiveInfinity;
    }
}