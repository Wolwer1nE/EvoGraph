using EvoGraph.Agent;
using EvoGraph.Epoch;
using EvoGraph.MathUtils;
using EvoGraph.Numeric;

namespace EvoGraphTest.AgentTest;

public class NumericGenAlgTest
{
    private Random _random = new Random();
    
    [Test]
    public void BasicNumericGenAlgTest()
    {
        const int count = 100;
        var agents = GetAgents(count);
        
        var manager = new SpeciesManagerExample(0.5, 0.2, 4, 
            8, agents.Count, 5);
        foreach (var agent in agents) manager.Add(agent);
        var settings = new EpochSettings(0.2, 0.2, 0.1, 0.3);
        var strategy = new OffspringStrategy(settings);
        var genAlg = new NumericGenAlg(manager, FitnessFunctionExample.CountFitnessParaboloid, settings, strategy);
        
        var bestFitness = GetBestFitness(genAlg, count);
        Assert.That(bestFitness < 0.001);
    }
    
    [Test]
    public void RosenbrockNumericGenAlgTest()
    {
        const int count = 500;
        var agents = GetAgents(count);
        
        var manager = new SpeciesManagerExample(0.5, 0.2, 4, 
            10, agents.Count, 5);
        //var manager = SpeciesManagerExample.NoSpeciesManager(count);
        foreach (var agent in agents) manager.Add(agent);
        var settings = new EpochSettings(0.35, 0.2, 0.25, 0.01);
        var strategy = new OffspringStrategy(settings);
        var genAlg = new NumericGenAlg(manager, FitnessFunctionExample.CountFitnessRosenbrock, settings, strategy);
        
        var bestFitness = GetBestFitness(genAlg, 1000);
        Assert.That(bestFitness < 0.001);
    }
    
    [Test]
    public void HimmelblauNumericGenAlgTest()
    {
        const int count = 500;
        var agents = GetAgents(count);
        
        var manager = new SpeciesManagerExample(0.5, 0.2, 4, 
            10, agents.Count, 5);
        //var manager = SpeciesManagerExample.NoSpeciesManager(count);
        foreach (var agent in agents) manager.Add(agent);
        var settings = new EpochSettings(0.35, 0.2, 0.25, 0.01);
        var strategy = new OffspringStrategy(settings);
        var genAlg = new NumericGenAlg(manager, FitnessFunctionExample.CountFitnessHimmelblau, settings, strategy);
        
        var bestFitness = GetBestFitness(genAlg, 1000);
        Assert.That(bestFitness < 0.001);
    }

    private List<IAgent> GetAgents(int count)
    {
        var agents = new List<IAgent>();
        for (var i = 0; i < count; i++)
        {
            var x = 5000 - _random.NextDouble() * 10000;
            var y = 5000 - _random.NextDouble() * 10000;
            agents.Add(new AgentExample(x, y));
        }
        return agents;
    }

    private double GetBestFitness(NumericGenAlg alg, int maxIterations)
    {
        var bestFitness = double.MaxValue;
        var lim = Math.Max(maxIterations / 100, 1);
        for (var i = 0; i < maxIterations; i++)
        {
            bestFitness = alg.Step().BestFitness;
            if (i % lim == 0) TestContext.Out.WriteLine($"Step: {i}; Best Fitness: {bestFitness}");
            if (bestFitness == 0)
            {
                TestContext.Out.WriteLine($"Step: {i}; Best Fitness: 0");
                break;
            }
        }
        return bestFitness;
    }
}
