namespace CodingKata.Parrots;

// extracted European parrot related logic into separate class
// simplified ctor to include only required arguments
// extracted 'magic' numbers into const fields

public class NorwegianBlueParrot : IParrot
{    
    private const double ZeroSpeed = 0d;

    // plus 5 points if this magic number is called correctly
    private const double MaxSpeed = 24d;

    public ParrotType Type => ParrotType.NorwegianBlue;

    private readonly double _voltage;
    private readonly bool _isNailed;
    private readonly ParrotFitness _fitness;

    public NorwegianBlueParrot(double voltage, bool isNailed, ParrotFitness? fitness = default)
    {
        _voltage = voltage;
        _isNailed = isNailed;
        _fitness = fitness ?? ParrotFitness.Default;
    }

    public double GetSpeed()
    {
        return _isNailed 
            ? ZeroSpeed 
            : Math.Min(MaxSpeed, _voltage * _fitness.Speed);
    }
}