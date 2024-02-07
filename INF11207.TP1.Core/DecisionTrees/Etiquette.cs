namespace INF11207.TP1.Core;

public class Etiquette : IDecision
{
    private readonly string _valeur;

    public Etiquette(string valeur)
    {
        _valeur = valeur;
    }

    public bool Equals(ValeurAttribut valeur)
    {
        return _valeur.Equals(valeur);
    }
}
