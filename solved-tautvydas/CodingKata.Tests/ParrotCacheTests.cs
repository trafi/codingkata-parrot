using System;
using System.Linq;
using System.Threading.Tasks;
using CodingKata.Parrots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingKata.Tests;

[TestClass]
public class ParrotCacheTests
{
    private int _loadParrotDelayedCount;
    private int _loadParrotCount;

    private InMemoryParrotCache _parrotCache = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _loadParrotDelayedCount = 0;
        _loadParrotCount = 0;

        _parrotCache = new InMemoryParrotCache(TimeSpan.FromMilliseconds(10));
    }

    [TestCleanup]
    public void Cleanup()
    {
        _parrotCache.Dispose();
    }

    [TestMethod]
    public async Task GetFromCacheOnce()
    {
        var result = await _parrotCache.GetOrAddParrot("parrot", LoadParrot);
        Assert.IsNotNull(result);
        Assert.AreEqual(ParrotType.European, result.Type);
        Assert.AreEqual(1, _loadParrotCount);
    }

    [TestMethod]
    public async Task GetFromCacheTwice()
    {
        var result1 = await _parrotCache.GetOrAddParrot("parrot", LoadParrot);
        var result2 = await _parrotCache.GetOrAddParrot("parrot", LoadParrot);

        Assert.IsNotNull(result1);
        Assert.AreEqual(ParrotType.European, result1.Type);
        Assert.AreEqual(result1, result2);
        Assert.AreEqual(1, _loadParrotCount);
    }

    [TestMethod]
    public async Task GetFromCacheDifferent()
    {
        var result1 = await _parrotCache.GetOrAddParrot("parrot1", LoadParrot);
        var result2 = await _parrotCache.GetOrAddParrot("parrot2", LoadParrot);

        Assert.IsNotNull(result1);
        Assert.AreEqual(ParrotType.European, result1.Type);

        Assert.IsNotNull(result2);
        Assert.AreEqual(ParrotType.European, result2.Type);

        Assert.AreNotEqual(result1, result2);
        Assert.AreEqual(2, _loadParrotCount);
    }

    [TestMethod]
    public async Task GetFromCacheExpired()
    {
        var result1 = await _parrotCache.GetOrAddParrot("parrot", LoadParrot);
        await Task.Delay(TimeSpan.FromMilliseconds(10));
        var result2 = await _parrotCache.GetOrAddParrot("parrot", LoadParrot);

        Assert.IsNotNull(result1);
        Assert.AreEqual(ParrotType.European, result1.Type);

        Assert.IsNotNull(result2);
        Assert.AreEqual(ParrotType.European, result2.Type);

        Assert.AreNotEqual(result1, result2);
        Assert.AreEqual(2, _loadParrotCount);
    }

    [TestMethod]
    public async Task GetFromCacheMultiThreaded()
    {
        var tasks = Enumerable.Range(0, 100).Select(_ => _parrotCache.GetOrAddParrot("parrot", LoadParrotDelayed))
            .ToList();
        var results = await Task.WhenAll(tasks);

        Assert.AreEqual(1, _loadParrotDelayedCount);
        var firstParrot = results.First();
        foreach (var result in results)
            Assert.AreEqual(result, firstParrot);
    }

    [TestMethod]
    public async Task GetFromCacheAfterCleanUp()
    {
        _parrotCache.Dispose();
        _parrotCache = new InMemoryParrotCache(
            TimeSpan.FromMilliseconds(10),
            TimeSpan.FromMilliseconds(5),
            TimeSpan.FromMilliseconds(5));

        await _parrotCache.GetOrAddParrot("parrot", LoadParrot);
        await _parrotCache.GetOrAddParrot("parrot", LoadParrot);

        await Task.Delay(20);
        // break point here to analyse memory, check if queue and cache is empty
        // or use reflections

        Assert.AreEqual(1, _loadParrotCount);
    }

    private async Task<IParrot> LoadParrotDelayed()
    {
        _loadParrotDelayedCount++;
        await Task.Delay(100); // simulate network
        return new EuropeanParrot();
    }

    private Task<IParrot> LoadParrot()
    {
        _loadParrotCount++;
        return Task.FromResult<IParrot>(new EuropeanParrot());
    }
}