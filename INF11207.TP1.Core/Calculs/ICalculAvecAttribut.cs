namespace INF11207.TP1.Core.Calculs;

public interface ICalculAvecAttribut : ICalculSurEnsemble
{
    double Calculer(Ensemble ensemble, string attribut);
}