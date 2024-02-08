namespace INF11207.TP1.Core;

public class Intervalle : IDecision
{
    private readonly Tuple<double, double> _intervalle;

    public Intervalle(Tuple<double, double> intervalle)
    {
        _intervalle = intervalle;
    }

    public Intervalle(double borneInf, double borneSup)
    {
        _intervalle = new Tuple<double, double>(borneInf, borneSup);
    }

    public bool Equals(ValeurAttribut valeur)
    {
        return valeur.ValeurDiscrete >= _intervalle.Item1 && valeur.ValeurDiscrete < _intervalle.Item2;
    }
    
    public override string ToString()
    {
        return $"{_intervalle.Item1} à {_intervalle.Item2}";
    }
}
