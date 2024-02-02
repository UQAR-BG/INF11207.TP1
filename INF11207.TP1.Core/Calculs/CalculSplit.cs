namespace INF11207.TP1.Core.Calculs;

public class CalculSplit : ICalculAvecAttribut, IIdentifiable
{
    public static string Id => "CalculSplit";
    
    public double Calculer(Ensemble ensemble)
    {
        return Calculer(ensemble, "");
    }

    public double Calculer(Ensemble ensemble, string attribut)
    {
        double retour = 0;
        
        foreach (string valeur in ensemble.ValeursPossiblesAttribut(attribut))
        {
            Ensemble sousEnsemble = ensemble.SousEnsembleAttribut(attribut, valeur);

            retour += sousEnsemble.Length * Math.Log2(sousEnsemble.Entropie);
        }

        return Math.Log2(ensemble.Length) - retour / ensemble.Length;
    }
}