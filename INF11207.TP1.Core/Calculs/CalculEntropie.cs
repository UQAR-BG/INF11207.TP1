namespace INF11207.TP1.Core.Calculs;

public class CalculEntropie : ICalculEntropie, IIdentifiable
{
    public static string Id => "CalculEntropie";
    
    public double Calculer(Ensemble ensemble)
    {
        double retour = 0;

        foreach (string etiquette in ensemble.Etiquettes)
        {
            Ensemble sousEnsemble = ensemble.SousEnsembleEtiquette(etiquette);
            int longueurSousEnsemble = sousEnsemble.Length;

            retour += longueurSousEnsemble * Math.Log2(longueurSousEnsemble);
        }

        return Math.Log2(ensemble.Length) - (retour / ensemble.Length);
    }
}