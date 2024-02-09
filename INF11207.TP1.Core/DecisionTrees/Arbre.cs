using INF11207.TP1.Core.Calculs;
using INF11207.TP1.Core.Exceptions;

namespace INF11207.TP1.Core;

public abstract class Arbre
{
    private readonly ICalculAttributOptimal _attributOptimal;
    private readonly string _cheminElagage;

    public Ensemble Ensemble {  get; protected set; }
    public NoeudAbstrait Racine { get; protected set; }

    public Arbre(ICalculAttributOptimal strategie, string chemin = "", string cheminElagage = "")
    {
        _attributOptimal = strategie;
        _cheminElagage = cheminElagage;
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
            noeudActuel = noeudActuel.Follow(valeur);
        }

        return noeudActuel.Etiquette;
    }

    public void Elaguer()
    {
        if (!string.IsNullOrEmpty(_cheminElagage))
        {
            ElaguerNoeud(Racine, new Ensemble(_cheminElagage));
        }
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

        var sauvegardeValeurs = ensemble.SauvegarderValeursDiscretes();

        ensemble = PreparerDonnees(ensemble);
        string aTester = ChoisirAttributATester(ensemble);
        
        NoeudAbstrait noeud = new Noeud(aTester);

        if (sauvegardeValeurs.Any(t => t.Item1.Equals(aTester)))
        {
            foreach (var intervalle in ensemble.Discretiser(aTester))
            {
                IDecision branche = new Intervalle(intervalle);
                Ensemble sousEnsemble = ensemble.SousEnsembleAttribut(aTester, branche);

                noeud[branche] = ConstruireArbre(sousEnsemble);
            }
        }
        else
        {
            foreach (string valeur in ensemble.ValeursPossiblesAttribut(aTester))
            {
                IDecision branche = new Etiquette(valeur);
                Ensemble sousEnsemble = ensemble.SousEnsembleAttribut(aTester, branche);

                noeud[branche] = ConstruireArbre(sousEnsemble);
            }
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

                foreach (IDecision branche in arbre.Enfants.Keys)
                {
                    Console.WriteLine(new string('\t', nbTabs) + '-' + branche);
                    AfficherArbre(arbre[branche], nbTabs + 1);
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

    private NoeudAbstrait ElaguerNoeud(NoeudAbstrait noeud, Ensemble prévisions)
    {
        if (noeud.GetType() == typeof(Feuille))
            return noeud;

        double proportionInitiale = TauxErreur(prévisions);

        return new Feuille("");
    }

    private double TauxErreur(Ensemble ensemble)
    {
        int nombreDeFaux = 0;

        foreach (Exemple exemple in ensemble.Exemples)
        {
            string entrainement = exemple.Etiquette;
            string prevision = Etiqueter(exemple);

            if (!entrainement.Equals(prevision)) nombreDeFaux++;
        }

        return nombreDeFaux / (double)ensemble.Length;
    }
}