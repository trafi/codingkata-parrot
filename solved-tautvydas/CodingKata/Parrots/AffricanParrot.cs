namespace CodingKata.Parrots;

// extracted Affrican parrot related logic into separate class
// simplified ctor to include only required arguments
// extracted 'magic' numbers into const fields

public class AffricanParrot : IParrot
{    
    private const double ZeroSpeed = 0d;

    public ParrotType Type => ParrotType.Affrican;

    private readonly int _numberOfCoconuts;
    private readonly ParrotFitness _fitness;

    public AffricanParrot(int numberOfCoconuts, ParrotFitness? fitness = default)
    {
        _numberOfCoconuts = numberOfCoconuts;
        _fitness = fitness ?? ParrotFitness.Default;
    }

    public double GetSpeed()
    {
        var speed = _fitness.Speed - _fitness.LoadFactor * _numberOfCoconuts;
        return Math.Max(ZeroSpeed, speed);
    }
}