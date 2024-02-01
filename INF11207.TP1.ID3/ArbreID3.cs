using INF11207.TP1.Core.Exceptions;

namespace INF11207.TP1.ID3;

public class ArbreID3
{
    public Ensemble Ensemble {  get; init; }
    public NoeudAbstrait Arbre { get; private set; }

    public ArbreID3 (string chemin = "")
    {
        Ensemble = new Ensemble(chemin);
    }

    public void Construire()
    {
        Arbre = ConstruireArbre(Ensemble);
    }

    public void Afficher()
    {
        AfficherArbre(Arbre);
    }

    public string Etiqueter(Exemple exemple)
    {
        NoeudAbstrait noeudActuel = Arbre;
        string nomAttribut;

        while (!noeudActuel.IsFeuille)
        {
            nomAttribut = exemple.Attribut(noeudActuel.Etiquette);
            noeudActuel = noeudActuel.Enfants[nomAttribut];
        }

        return noeudActuel.Etiquette;
    }

    private NoeudAbstrait ConstruireArbre(Ensemble ensemble)
    {
        if (ensemble.IsEmpty)
            throw new EnsembleVideException();

        if (ensemble.Entropie == 0)
            return new Feuille(ensemble.Exemples.First().Etiquette);

        Ensemble sousEnsemble;

        if (ensemble.Largeur == 0)
        {
            int max = 0;
            string etiquettePlusFrequente = string.Empty;

            foreach(string etiquette in ensemble.EtiquettesPossibles())
            {
                sousEnsemble = ensemble.SousEnsembleEtiquette(etiquette);
                if (sousEnsemble.Length > max)
                {
                    max = sousEnsemble.Length;
                    etiquettePlusFrequente = etiquette;
                }
            }
            return new Feuille(etiquettePlusFrequente);
        }

        string aTester = ensemble.AttributOptimal();
        NoeudAbstrait noeud = new Noeud(aTester);

        foreach (string valeur in ensemble.ValeursPossiblesAttribut(aTester))
        {
            sousEnsemble = ensemble.SousEnsembleAttribut(aTester, valeur);

            noeud.Enfants[valeur] = ConstruireArbre(sousEnsemble);
        }

        return noeud;
    }

    private void AfficherArbre(NoeudAbstrait arbre, int nbTabs = 0)
    {
        Type type = arbre.GetType();
        switch (type)
        {
            case Type _ when type == typeof(Noeud):
                Console.WriteLine(new string('\t', nbTabs) + arbre.Etiquette);

                foreach (string enfant in arbre.Enfants.Keys)
                {
                    Console.WriteLine(new string('\t', nbTabs) + '-' + enfant);
                    AfficherArbre(arbre.Enfants[enfant], nbTabs + 1);
                }
                break;
            case Type _ when type == typeof(Feuille):
                Console.WriteLine(new string('\t', nbTabs) + '.' + arbre.Etiquette);
                break;
            default:
                throw new TypeInexistantException($"noeud doit être un Noeud/Feuille et pas un {type}");
        }
    }
}
