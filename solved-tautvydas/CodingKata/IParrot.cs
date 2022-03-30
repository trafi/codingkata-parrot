namespace CodingKata;

// introduced new interface so we could split business logic from Parrot class
// we could achieve similar behaviour with abstract class

// what's the difference between interface and abstract class?
// what if abstract class contains only virtual methods?
// what if interface contains default implementation?

public interface IParrot
{
    ParrotType Type { get; }
    double GetSpeed();
}