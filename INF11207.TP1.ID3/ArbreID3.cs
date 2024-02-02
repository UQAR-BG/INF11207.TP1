using INF11207.TP1.Core;

namespace INF11207.TP1.ID3;

public class ArbreID3 : Arbre
{
    public ArbreID3 (string chemin = "")
    {
        Ensemble = new Ensemble(chemin);
    }

    protected override string ChoisirAttributATester(Ensemble ensemble)
    {
        return ensemble.AttributOptimal();
    }
}
