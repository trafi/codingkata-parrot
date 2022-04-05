namespace CodingKata;

public interface IParrotCache {
    Task<Parrot> GetOrAddParrot(string parrotId, Func<Task<Parrot>> factory);
}

