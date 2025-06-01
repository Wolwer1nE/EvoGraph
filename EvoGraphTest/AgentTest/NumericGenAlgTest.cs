using EvoGraph.Agent;
using EvoGraph.Numeric;
using EvoGraph.Random;

namespace EvoGraphTest.AgentTest;

public class NumericGenAlgTest
{
    [Test]
    public void BasicNumericGenAlgTest()
    {
        int count = 200;
        var agents = new List<IAgent>();
        for (int i = 0; i < count; i++)
        {
            var x = 50 - Rnd.NextDouble(100);
            var y = 50 - Rnd.NextDouble(100);
            agents.Add(new AgentExample(x, y));
        }
        var genAlg = new NumericGenAlg(agents);
        for (int i = 0; i < count; i++)
        {
            Console.Write("Step: " + i + "; Best Fitness: ");
            Console.WriteLine(genAlg.Step().BestFitness);
        }
        Assert.That(genAlg.Step().BestFitness > 100000);
    }
}