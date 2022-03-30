namespace CodingKata;

public interface IParrotCache {
    Task<IParrot> GetOrAddParrot(string parrotId, Func<Task<IParrot>> factory);
}