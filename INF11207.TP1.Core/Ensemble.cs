using INF11207.TP1.Core.Calculs;
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
                _entropie = Registry.CreateAndRegister<CalculEntropie>(CalculEntropie.Id)
                    .Calculer(this);
            return _entropie;
        }
    }

    private IList<string> _etiquettes;
    public IList<string> Etiquettes
    {
        get
        {
            if (NeedsUpdating)
                _etiquettes = new UniqueList<string>(
                    Exemples.Select(e => e.Etiquette));
            return _etiquettes;
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
            string[] attributs = ligne.Split(';');
            string etiquette = attributs.Last();
            if (attributs.Length == nomAttributs.Count)
                nomAttributs.RemoveAt(attributs.Length - 1);

            retour.Add(new Exemple(nomAttributs, 
                attributs[0..nomAttributs.Count].ToList(), 
                etiquette));
        }

        return retour;
    }

    public Ensemble SousEnsembleEtiquette(string etiquette)
    {
        return new Ensemble
        {
            Attributs = Attributs,
            Exemples = Exemples.Where(e => e.Etiquette.Equals(etiquette)).ToList()
        };
    }

    public IList<string> ValeursPossiblesAttribut(string nomAttribut)
    {
        return new UniqueList<string>(
            Exemples.Select(e => e.GetValeur(nomAttribut)));
    }

    public Ensemble SousEnsembleAttribut(string nomAttribut, string valeur)
    {
        return SousEnsembleAttribut(nomAttribut, new Etiquette(valeur));
    }

    public Ensemble SousEnsembleAttribut(string nomAttribut, IDecision decision)
    {
        Ensemble retour = new Ensemble();
        retour.Attributs = new List<string>(Attributs);
        retour.Attributs.Remove(nomAttribut);
        retour.Exemples = Exemples.Where(e => decision.Equals(e.GetValeur(nomAttribut))).ToList();

        return retour;
    }

    public IList<Tuple<string, IList<string>>> SauvegarderValeursDiscretes()
    {
        IList<Tuple<string, IList<string>>> retour = new List<Tuple<string, IList<string>>>();

        foreach (string attr in Attributs)
        {
            if (EstDiscretisable(attr))
            {
                IList<string> valeurs = Exemples.Select(e => e.GetValeur(attr)).ToList();
                retour.Add(new Tuple<string, IList<string>>(attr, valeurs));
            }
        }
        
        return retour;
    }

    public List<Tuple<double, double>> Discretiser(string attribut)
    {
        List<Tuple<double, double>> listeIntervalles = new();

        List<Tuple<int, double>> valeursTriees = new();
        for (int i = 0; i < Length; i++)
            valeursTriees.Add(new Tuple<int, double>(i, double.Parse(Exemples[i].GetValeur(attribut))));

        valeursTriees.Sort((a, b) => a.Item2.CompareTo(b.Item2));
        int indiceBorneInf = 0;
        double borneInf = double.NegativeInfinity;
        string classe = Exemples[valeursTriees[0].Item1].Etiquette;

        for (int i = 1; i < valeursTriees.Count; i++)
        {
            double borneSup;

            if (!Exemples[valeursTriees[i].Item1].Etiquette.Equals(classe))
            {
                borneSup = (valeursTriees[i].Item2 +
                        valeursTriees[i - 1].Item2) / 2;

                if (borneInf < borneSup)
                {
                    listeIntervalles.Add(new Tuple<double, double>(borneInf, borneSup));
                    borneInf = borneSup;
                    classe = Exemples[valeursTriees[i].Item1].Etiquette;
                }

                indiceBorneInf = i;
            }
        }

        listeIntervalles.Add(new Tuple<double, double>(borneInf, double.PositiveInfinity));
        return listeIntervalles;
    }

    private bool EstDiscretisable(string attribut)
    {
        int i = 0;
        bool tousDiscretisable = true;
        while (tousDiscretisable && i < Exemples.Count)
        {
            tousDiscretisable = double.TryParse(Exemples[i].GetValeur(attribut), out var result);
            i++;
        }

        return tousDiscretisable;
    }
}
