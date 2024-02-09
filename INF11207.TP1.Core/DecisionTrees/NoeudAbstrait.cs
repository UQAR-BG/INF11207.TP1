namespace INF11207.TP1.Core;

public abstract class NoeudAbstrait
{
    private Dictionary<IDecision, NoeudAbstrait> _enfants { get; init; }
    public Dictionary<IDecision, NoeudAbstrait> Enfants { get => _enfants; }
    public NoeudAbstrait this[IDecision valeur]
    {
        get => _enfants[valeur];
        set => _enfants[valeur] = value;
    }

    public string Etiquette { get; init; }
    public bool IsFeuille { get => GetType() == typeof(Feuille); }

    public NoeudAbstrait Follow(string valeur)
    {
        foreach (var decision in _enfants.Keys)
        {
            if (decision.Equals(valeur))
                return _enfants[decision];
        }

        throw new Exception("Node not found");
    }

    public NoeudAbstrait(string etiquette)
    {
        Etiquette = etiquette;
        _enfants = new Dictionary<IDecision, NoeudAbstrait>();
    }
}
