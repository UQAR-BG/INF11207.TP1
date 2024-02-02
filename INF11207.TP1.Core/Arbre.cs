using INF11207.TP1.Core.Calculs;
using INF11207.TP1.Core.Exceptions;

namespace INF11207.TP1.Core;

public abstract class Arbre
{
    private readonly ICalculAttributOptimal _attributOptimal;
    
    public Ensemble Ensemble {  get; protected set; }
    public NoeudAbstrait Racine { get; protected set; }

    public Arbre(ICalculAttributOptimal strategie, string chemin = "")
    {
        _attributOptimal = strategie;
        Ensemble = new Ensemble(chemin);
    }
    
    public virtual void Construire()
    {
        Racine = ConstruireArbre(Ensemble);
    }

    public void Afficher()
    {
        AfficherArbre(Racine);
    }

    public virtual string Etiqueter(Exemple exemple)
    {
        NoeudAbstrait noeudActuel = Racine;
        string valeur;

        while (!noeudActuel.IsFeuille)
        {
            valeur = exemple.GetValeur(noeudActuel.Etiquette);
            noeudActuel = noeudActuel.Enfants[valeur];
        }

        return noeudActuel.Etiquette;
    }
    
    protected virtual Ensemble PreparerDonnees(Ensemble ensemble)
    {
        return ensemble;
    }

    private NoeudAbstrait ConstruireArbre(Ensemble ensemble)
    {
        if (ensemble.IsEmpty)
            throw new EnsembleVideException();

        if (ensemble.Entropie == 0)
            return new Feuille(ensemble.Exemples.First().Etiquette);

        if (ensemble.Largeur == 0)
            return GenererFeuilleAPartirDe(ensemble);

        ensemble = PreparerDonnees(ensemble);
        string aTester = ChoisirAttributATester(ensemble);
        
        NoeudAbstrait noeud = new Noeud(aTester);

        foreach (string valeur in ensemble.ValeursPossiblesAttribut(aTester))
        {
            Ensemble sousEnsemble = ensemble.SousEnsembleAttribut(aTester, valeur);

            noeud.Enfants[valeur] = ConstruireArbre(sousEnsemble);
        }

        return noeud;
    }

    private string ChoisirAttributATester(Ensemble ensemble)
    {
        return _attributOptimal.Calculer(ensemble);
    }

    private void AfficherArbre(NoeudAbstrait arbre, int nbTabs = 0)
    {
        Type type = arbre.GetType();
        switch (type)
        {
            case not null when type == typeof(Noeud):
                Console.WriteLine(new string('\t', nbTabs) + arbre.Etiquette);

                foreach (string enfant in arbre.Enfants.Keys)
                {
                    Console.WriteLine(new string('\t', nbTabs) + '-' + enfant);
                    AfficherArbre(arbre.Enfants[enfant], nbTabs + 1);
                }

                break;
            case not null when type == typeof(Feuille):
                Console.WriteLine(new string('\t', nbTabs) + '.' + arbre.Etiquette);
                break;
            default:
                throw new TypeInexistantException($"noeud doit être un Noeud/Feuille et pas un {type}");
        }
    }

    private Feuille GenererFeuilleAPartirDe(Ensemble ensemble)
    {
        int max = 0;
        string etiquettePlusFrequente = string.Empty;

        foreach(string etiquette in ensemble.Etiquettes)
        {
            Ensemble sousEnsemble = ensemble.SousEnsembleEtiquette(etiquette);
            if (sousEnsemble.Length > max)
            {
                max = sousEnsemble.Length;
                etiquettePlusFrequente = etiquette;
            }
        }
        return new Feuille(etiquettePlusFrequente);
    }
}