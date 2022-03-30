namespace CodingKata.Parrots;

// extracted European parrot related logic into separate class
// simplified ctor to include only required arguments

public class EuropeanParrot : IParrot
{    
    public ParrotType Type => ParrotType.European;

    private readonly ParrotFitness _fitness;

    public EuropeanParrot(ParrotFitness? fitness = default)
    {
        _fitness = fitness ?? ParrotFitness.Default;
    }

    public double GetSpeed()
    {
        return _fitness.Speed;
    }
}