using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingKata.Tests;

[TestClass]
public class UnitTest1
{
    private ParrotFitness fitness = new ParrotFitness (5, 5);

    [TestMethod]
    public void AffricanParrotTest()
    {
        var parrot = new AffricanParrot(1);
        Assert.AreEqual(3, parrot.GetSpeed());
    }

    [TestMethod]
    public void EuropeanParrotTest()
    {
        var parrot = new EuropeanParrot();
        Assert.AreEqual(12, parrot.GetSpeed());
    }
    
    [TestMethod]
    public void EuropeanParrotTestWithFitness()
    {
        var parrot = new EuropeanParrot(fitness);
        Assert.AreEqual(5, parrot.GetSpeed());
    }
    
    [TestMethod]
    public void NailedNorwegianBlueParrotTest()
    {
        var parrot = new NorwegianBlueParrot(0, true);
        Assert.AreEqual(0, parrot.GetSpeed());
    }    
    
    [TestMethod]
    public void NorwegianBlueParrotTest()
    {
        var parrot = new NorwegianBlueParrot(0.5, false);
        Assert.AreEqual(6, parrot.GetSpeed());
    }    
}