namespace CodingKata;

public class EuropeanParrot : Parrot
{

    public EuropeanParrot(ParrotFitness? fitness = default) : base(fitness) {
    }

    public override double GetSpeed()
    {
        return GetBaseSpeed();
    }
}