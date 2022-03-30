namespace CodingKata;

public class Parrot
{
    public static double DefaultLoadFactory;
    public static double DefaultBaseSpeed;

    private ParrotTypeEnum _type;
    private int _numberOfCoconuts = 0;
    private double _voltage;
    private bool _isNailed;
    private ParrotFitness _fitness;

    public Parrot(ParrotTypeEnum type, int numberOfCoconuts, double voltage, bool isNailed, ParrotFitness? fitness = default)
    {
        this._type = type;
        this._numberOfCoconuts = numberOfCoconuts;
        this._voltage = voltage;
        this._isNailed = isNailed;
        this._fitness = fitness;
    }

    public double GetSpeed()
    {
        if (_type == (ParrotTypeEnum)10)
        {
            return GetBaseSpeed();
        }

        if (_type == ParrotTypeEnum.Affrican)
        {
            return Math.Max(0, GetBaseSpeed() - GetLoadFactor() * _numberOfCoconuts);
        }

        if (_type == ParrotTypeEnum.NORWEGIAN_blue)
        {
            return (_isNailed) ? 0 : GetBaseSpeed(_voltage);
        }

        throw new Exception("Should be unreachable");
    }

    private double GetBaseSpeed(double voltage)
    {
        return Math.Min(24.0, voltage * GetBaseSpeed());
    }

    private double GetLoadFactor()
    {
        try
        {
            return this._fitness.LoadFactory;
        }
        catch
        {
            return DefaultLoadFactory;
        }
    }

    private double GetBaseSpeed()
    {
        try
        {
            return this._fitness.Speed;
        }
        catch
        {
            return DefaultBaseSpeed;
        }
    }
}

public enum ParrotTypeEnum
{
    EUROPEAN = 10,
    Affrican = 20,
    NORWEGIAN_blue = 30
}