namespace INF11207.TP1.Core.Calculs;

public class CalculGain : ICalculAvecAttribut, IIdentifiable
{
    public static string Id => "CalculGain";
    
    public double Calculer(Ensemble ensemble)
    {
        return Calculer(ensemble, "");
    }

    public double Calculer(Ensemble ensemble, string attribut)
    {
        double somme = 0;

        foreach (string valeur in ensemble.ValeursPossiblesAttribut(attribut))
        {
            Ensemble sousEnsemble = ensemble.SousEnsembleAttribut(attribut, valeur);

            somme += sousEnsemble.Length * sousEnsemble.Entropie;
        }

        return ensemble.Entropie - (somme / ensemble.Length);
    }
}