using EvoGraph.Numeric;

namespace EvoGraphTest;

public class HealthCheck
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void DummyTest()
    {
        var genAlg = new NumericGenAlg();
        Assert.That(genAlg.Step().BestFitness.Equals(0.0));
    }
}