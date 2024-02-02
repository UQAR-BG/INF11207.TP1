namespace INF11207.TP1.Core;

public abstract class Arbre : IArbre
{
    public Ensemble Ensemble {  get; init; }
    public NoeudAbstrait Racine { get; protected set; }
    
    public virtual void Construire()
    {
        throw new NotImplementedException();
    }

    public virtual void Afficher()
    {
        throw new NotImplementedException();
    }

    public virtual string Etiqueter(Exemple exemple)
    {
        throw new NotImplementedException();
    }
}