using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingKata.Tests;

[TestClass]
public class UnitTest1
{
    private ParrotFitness fitness = new ParrotFitness (5, 5);

    [TestMethod]
    public void AffricanParrotTest()
    {
        var parrot = new Parrot(ParrotTypeEnum.Affrican, 1, 0, false);
        Assert.AreEqual(3, parrot.GetSpeed());
    }

    [TestMethod]
    public void EuropeanParrotTest()
    {
        var parrot = new Parrot(ParrotTypeEnum.EUROPEAN, 0, 0, false);
        Assert.AreEqual(12, parrot.GetSpeed());
    }
    
    [TestMethod]
    public void EuropeanParrotTestWithFitness()
    {
        var parrot = new Parrot(ParrotTypeEnum.EUROPEAN, 0, 0, false, fitness);
        Assert.AreEqual(5, parrot.GetSpeed());
    }
    
    [TestMethod]
    public void NailedNorwegianBlueParrotTest()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_blue, 0, 0, true);
        Assert.AreEqual(0, parrot.GetSpeed());
    }    
    
    [TestMethod]
    public void NorwegianBlueParrotTest()
    {
        var parrot = new Parrot(ParrotTypeEnum.NORWEGIAN_blue, 0, 0.5, false);
        Assert.AreEqual(6, parrot.GetSpeed());
    }    
}