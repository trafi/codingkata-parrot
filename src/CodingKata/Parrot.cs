namespace CodingKata;

public abstract class Parrot
{
    public static double DefaultLoadFactory;
    public static double DefaultBaseSpeed;

    protected ParrotFitness _fitness;

    public Parrot(ParrotFitness? fitness = default)
    {
        this._fitness = fitness;
    }

    public abstract double GetSpeed();

    protected double GetLoadFactor()
    {
        return this._fitness?.LoadFactory ?? DefaultLoadFactory;
    }

    protected double GetBaseSpeed()
    {
        return this._fitness?.Speed ?? DefaultBaseSpeed;
    }
}