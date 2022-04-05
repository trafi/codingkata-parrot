namespace CodingKata;

public class NorwegianBlueParrot : Parrot
{
    private bool _isNailed;
    private double _voltage;


    public NorwegianBlueParrot(double voltage, bool isNailed, ParrotFitness? fitness = default) : base()
    {
        this._isNailed = isNailed;
        base._fitness = fitness;
        this._voltage = voltage;
    }

    private double GetBaseSpeed(double voltage)
    {
        return Math.Min(24.0, voltage * GetBaseSpeed());
    }

    public override double GetSpeed()
    {
        return (_isNailed) ? 0 : GetBaseSpeed(_voltage);
    }
}