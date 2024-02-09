using INF11207.TP1.Core;
using INF11207.TP1.Core.Calculs;

namespace INF11207.TP1.C45;

public class ArbreC45 : Arbre
{
    public ArbreC45 (string chemin = "", string cheminElagage = "") : 
        base(Registry.CreateAndRegister<CalculAttributOptimalC45>(CalculAttributOptimalC45.Id), chemin, cheminElagage) { }
}