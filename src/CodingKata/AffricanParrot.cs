namespace CodingKata;

public class AffricanParrot : Parrot
{
    private int _numberOfCoconuts = 0;

    public AffricanParrot(int numberOfCoconuts, ParrotFitness? fitness = default) : base() {
        this._numberOfCoconuts = numberOfCoconuts;
        base._fitness = fitness;
    }

    public override double GetSpeed()
    {
        return Math.Max(0, GetBaseSpeed() - GetLoadFactor() * _numberOfCoconuts);
    }
}