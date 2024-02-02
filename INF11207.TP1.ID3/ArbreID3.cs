using INF11207.TP1.Core;
using INF11207.TP1.Core.Calculs;

namespace INF11207.TP1.ID3;

public class ArbreID3 : Arbre
{
    public ArbreID3 (string chemin = "") : 
        base(Registry.CreateAndRegister<CalculAttributOptimalID3>(CalculAttributOptimalID3.Id), chemin) { }
}
