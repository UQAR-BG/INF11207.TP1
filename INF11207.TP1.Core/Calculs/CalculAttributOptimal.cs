namespace INF11207.TP1.Core.Calculs;

public abstract class CalculAttributOptimal : ICalculAttributOptimal
{
    public static string Id => "CalculAttributOptimal";
    
    private readonly ICalculAvecAttribut _gain;

    public CalculAttributOptimal(ICalculAvecAttribut gain)
    {
        _gain = gain;
    }

    public string Calculer(Ensemble ensemble)
    {
        double max = double.NegativeInfinity;
        string retour = string.Empty;

        foreach (string attribut in ensemble.Attributs)
        {
            double gain = _gain.Calculer(ensemble, attribut);
            if (gain >= max)
            {
                max = gain;
                retour = attribut;
            }
        }

        return retour;
    }
}