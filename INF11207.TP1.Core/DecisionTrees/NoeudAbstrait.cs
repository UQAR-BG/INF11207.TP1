namespace INF11207.TP1.Core;

public abstract class NoeudAbstrait
{
    public Dictionary<string, NoeudAbstrait> Enfants { get; init; }
    public string Etiquette { get; init; }
    public bool IsFeuille { get => GetType() == typeof(Feuille); }

    public NoeudAbstrait(string etiquette)
    {
        Etiquette = etiquette;
        Enfants = new Dictionary<string, NoeudAbstrait>();
    }

    //public NoeudAbstrait Follow(string valeur)
    //{
    //    foreach (var decision in _enfants.Keys)
    //    {
    //        if (decision.Equals(valeur))
    //            return _enfants[decision];
    //    }

    //    throw new Exception("Node not found");
    //}
}
