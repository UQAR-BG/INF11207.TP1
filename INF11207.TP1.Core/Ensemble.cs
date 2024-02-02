using INF11207.TP1.CsvFileHandling;

namespace INF11207.TP1.Core;

public class Ensemble : TrackableObject, IObserver
{
    private IList<string>? _attributs;
    public IList<string> Attributs 
    { 
        get => _attributs ??= new ObservableList<string>(this); 
        set 
        {
            if (value.GetType() != typeof(ObservableList<string>))
            {
                _attributs = new ObservableList<string>(this, value);
            }
            else
            {
                _attributs = value;
            }
            RegisterDirty();
        } 
    }

    private IList<Exemple>? _exemples;
    public IList<Exemple> Exemples
    {
        get => _exemples ??= new ObservableList<Exemple>(this);
        private set
        {
            if (value.GetType() != typeof(ObservableList<Exemple>))
            {
                _exemples = new ObservableList<Exemple>(this, value);
            }
            else
            {
                _exemples = value;
            }
            RegisterDirty();
        }
    }

    public int Length => Exemples.Count;
    public int Largeur => Attributs.Count;
    public bool IsEmpty => Length == 0;

    private double _entropie = 0;
    public double Entropie
    {
        get
        {
            if (NeedsUpdating)
                _entropie = CalculerEntropie();
            return _entropie;
        }
    }

    public Ensemble(string chemin = "")
    {
        if (!chemin.Equals(string.Empty))
        {
            Attributs = CsvReader.ReadAttributes(chemin);
            Exemples = ListeEnExemples(CsvReader.ReadLines(chemin), Attributs);
        }
        else
        {
            Attributs = new ObservableList<string>(this);
            Exemples = new ObservableList<Exemple>(this);
        }
    }

    public void Notify()
    {
        RegisterDirty();
    }

    public static IList<Exemple> ListeEnExemples(string[] exemples, IList<string> nomAttributs)
    {
        IList<Exemple> retour = new List<Exemple>();

        foreach (string ligne in exemples)
        {
            string[] attributs = ligne.Split(',');
            string etiquette = attributs.Length != nomAttributs.Count ? attributs.Last() : "";

            retour.Add(new Exemple(nomAttributs, 
                attributs[0..nomAttributs.Count].ToList(), 
                etiquette));
        }

        return retour;
    }

    public IList<string> EtiquettesPossibles()
    {
        IList<string> retour = new List<string>();

        foreach (Exemple exemple in Exemples)
        {
            if (!retour.Contains(exemple.Etiquette))
                retour.Add(exemple.Etiquette);
        }

        return retour;
    }

    public Ensemble SousEnsembleEtiquette(string etiquette)
    {
        Ensemble retour = new();
        retour.Attributs = Attributs;

        foreach (Exemple exemple in Exemples)
        {
            if (exemple.Etiquette.Equals(etiquette))
                retour.Exemples.Add(exemple);
        }

        return retour;
    }

    public string AttributOptimal(bool ID3 = true)
    {
        double max = double.NegativeInfinity;
        double gain;
        string retour = string.Empty;

        foreach (string attribut in Attributs)
        {
            gain = CalculerGainEntropie(attribut);

            if (gain >= max)
            {
                max = gain;
                retour = attribut;
            }
        }

        return retour;
    }

    public IList<string> ValeursPossiblesAttribut(string nomAttribut)
    {
        IList<string> retour = new List<string>();

        foreach (Exemple exemple in Exemples)
        {
            string attribut = exemple.Attributs[nomAttribut];

            if (!retour.Contains(attribut))
                retour.Add(attribut);
        }

        return retour;
    }

    public Ensemble SousEnsembleAttribut(string nomAttribut, string valeur)
    {
        Ensemble retour = new Ensemble();
        retour.Attributs = new List<string>(Attributs);
        retour.Attributs.Remove(nomAttribut);

        foreach (Exemple exemple in Exemples)
        {
            if (exemple.Attributs[nomAttribut].Equals(valeur))
                retour.Exemples.Add(exemple);
        }

        return retour;
    }

    private double CalculerEntropie()
    {
        double retour = 0;
        Ensemble sousEnsemble;
        int longueurSousEnsemble;

        foreach (string etiquette in EtiquettesPossibles())
        {
            sousEnsemble = SousEnsembleEtiquette(etiquette);
            longueurSousEnsemble = sousEnsemble.Length;

            retour += longueurSousEnsemble * Math.Log2(longueurSousEnsemble);
        }

        return Math.Log2(Length) - (retour / Length);
    }

    private double CalculerGainEntropie(string nomAttribut)
    {
        double somme = 0;
        Ensemble sousEnsemble;

        foreach (string valeur in ValeursPossiblesAttribut(nomAttribut))
        {
            sousEnsemble = SousEnsembleAttribut(nomAttribut, valeur);

            somme += sousEnsemble.Length * sousEnsemble.Entropie;
        }

        return Entropie - (somme / Length);
    }
}
