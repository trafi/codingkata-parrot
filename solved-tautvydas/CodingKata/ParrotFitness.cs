namespace CodingKata;

public class ParrotFitness
{
    // default values from static ctor moved to this field
    public static ParrotFitness Default => new ParrotFitness(12.0,  9.0);

    // made all properties readonly
    public double Speed { get; }

    public double LoadFactor { get; }

    // removed default ctor
    public ParrotFitness(double speed, double loadFactor)
    {
        Speed = speed;
        LoadFactor = loadFactor;
    }
}