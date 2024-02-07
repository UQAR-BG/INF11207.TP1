namespace INF11207.TP1.Core;

public class ValeurAttribut
{
    public string Valeur { get; private set; } = string.Empty;

    private double _valeurDiscrete;
    public double ValeurDiscrete { get => _valeurDiscrete; }

    private bool _estDiscrete;
    public bool EstDiscrete => _estDiscrete;

    public ValeurAttribut(string valeur)
    {
        if (!EvaluerSiDiscrete(valeur))
        {
            Valeur = valeur;
        }
    }

    public ValeurAttribut(double valeur)
    {
        _estDiscrete = true;
        _valeurDiscrete = valeur;
    }

    public override string ToString()
    {
        return Valeur;
    }

    private bool EvaluerSiDiscrete(string valeur)
    {
        _estDiscrete = double.TryParse(valeur, out _valeurDiscrete);
        return _estDiscrete;
    }
}
