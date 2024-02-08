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

    public bool Equals(string valeur)
    {
        if (!double.TryParse(valeur, out var val))
            return false;

        return val >= _intervalle.Item1 && val < _intervalle.Item2;
    }

    public override string ToString()
    {
        return $"{_intervalle.Item1} à {_intervalle.Item2}";
    }
}