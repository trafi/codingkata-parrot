namespace CodingKata;

public class ParrotFitness
{
    static ParrotFitness()
    {
        Parrot.DefaultBaseSpeed = 12.0;
        Parrot.DefaultLoadFactory = 9.0;
    }

    public ParrotFitness()
    {
    }

    public double Speed { get; set; }

    public ParrotFitness(double speed, double loadFactory)
    {
        Speed = speed;
        LoadFactory = loadFactory;
    }

    public double LoadFactory { get; set; }
}