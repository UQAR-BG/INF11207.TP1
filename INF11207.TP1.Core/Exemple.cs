﻿using INF11207.TP1.Core.Exceptions;

namespace INF11207.TP1.Core;

public class Exemple
{
    public Dictionary<string, ValeurAttribut> Attributs { get; init; }
    public string Etiquette { get; init; }

    public Exemple (IList<string> nomsAttributs, IList<string> valeursAttributs, string etiquette = "")
    {
        Etiquette = etiquette;
        Attributs = new Dictionary<string, ValeurAttribut> ();

        if (!AttributsCompatibles(nomsAttributs, valeursAttributs))
            throw new ExempleEtListAttributsIncompatibleException();

        for (int i = 0; i < nomsAttributs.Count; i++)
        {
            Attributs.Add(nomsAttributs[i], new ValeurAttribut(valeursAttributs[i]));
        }
    }

    public ValeurAttribut GetValeur(string nomAttribut) 
    {
        return Attributs[nomAttribut];
    }

    private bool AttributsCompatibles(IList<string> noms, IList<string> valeurs)
    {
        return noms.Count == valeurs.Count;
    }
}
