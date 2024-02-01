namespace INF11207.TP1.ID3;

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
}
