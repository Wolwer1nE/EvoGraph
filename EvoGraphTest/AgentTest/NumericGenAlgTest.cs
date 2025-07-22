using EvoGraph.Agent;
using EvoGraph.Numeric;
using EvoGraph.Random;

namespace EvoGraphTest.AgentTest;

public class NumericGenAlgTest
{
    [Test]
    public void BasicNumericGenAlgTest()
    {
        int count = 100;
        var agents = new List<IAgent>();
        for (int i = 0; i < count; i++)
        {
            var x = 5000 - Rnd.NextDouble(10000);
            var y = 5000 - Rnd.NextDouble(10000);
            agents.Add(new AgentExample(x, y));
        }
        var genAlg = new NumericGenAlg(agents, FitnessFunctionExample.CountFitness, new NumericGenAlgSettings());
        var bestFitness = Double.MaxValue;
        for (int i = 0; i < count; i++)
        {
            bestFitness = genAlg.Step().BestFitness;
            Console.WriteLine("Step: " + i + "; Best Fitness: " + bestFitness);
            if (bestFitness == 0) break;
        }
        Assert.That(bestFitness < 0.001);
    }
}