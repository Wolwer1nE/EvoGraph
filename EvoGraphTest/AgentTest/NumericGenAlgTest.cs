using EvoGraph.Agent;
using EvoGraph.Epoch;
using EvoGraph.MathUtils;
using EvoGraph.Numeric;

namespace EvoGraphTest.AgentTest;

public class NumericGenAlgTest
{
    [Test]
    public void BasicNumericGenAlgTest()
    {
        const int agentsNumber = 100;
        const int iterationsNumber = 100;
        
        // Due to the random in tests, assertation fails only if test failed three times in a row
        bool res = Test(agentsNumber, iterationsNumber);
        if (!res) res = Test(agentsNumber, iterationsNumber);
        if (!res) res = Test(agentsNumber, iterationsNumber);
        Assert.That(res, Is.True); 
        return;

        bool Test(int agentsCount, int maxIterations)
        {
            var agents = GetAgents(agentsCount, 5000);
        
            var speciesSettings = new SpeciesSettings(0.5, 0.2, 4, 10, agents.Count);
            var manager = new SpeciesManagerExample(speciesSettings);
            foreach (var agent in agents) manager.Add(agent);
            var settings = new OffspringSettings(0.2, 0.2, 0.1, 0.3);
            var strategy = new OffspringStrategy(settings);
            var genAlg = new NumericGenAlg(manager, FitnessFunctionExample.CountFitnessParaboloid, strategy);
        
            var bestFitness = GetBestFitness(genAlg, maxIterations);
            return bestFitness < 0.001;
        }
    }
    
    [Test]
    public void RosenbrockNumericGenAlgTest()
    {
        const int agentsNumber = 500;
        const int iterationsNumber = 1000;
        
        // Due to the random in tests, assertation fails only if test failed three times in a row
        bool res = Test(agentsNumber, iterationsNumber);
        if (!res) res = Test(agentsNumber, iterationsNumber);
        if (!res) res = Test(agentsNumber, iterationsNumber);
        Assert.That(res, Is.True);
        return;

        bool Test(int agentsCount, int maxIterations)
        {
            var agents = GetAgents(agentsCount, 1000);
        
            var speciesSettings = new SpeciesSettings(0.5, 0.2, 4, 10, agents.Count);
            var manager = new SpeciesManagerExample(speciesSettings);
            foreach (var agent in agents) manager.Add(agent);
            var settings = new OffspringSettings(0.35, 0.2, 0.25, 0.01);
            var strategy = new OffspringStrategy(settings);
            var genAlg = new NumericGenAlg(manager, FitnessFunctionExample.CountFitnessRosenbrock, strategy);
        
            var bestFitness = GetBestFitness(genAlg, maxIterations);
            return bestFitness < 0.001;
        }
    }
    
    [Test]
    public void HimmelblauNumericGenAlgTest()
    {
        const int agentsNumber = 500;
        const int iterationsNumber = 1000;
        
        // Due to the random in tests, assertation fails only if test failed three times in a row
        bool res = Test(agentsNumber, iterationsNumber);
        if (!res) res = Test(agentsNumber, iterationsNumber);
        if (!res) res = Test(agentsNumber, iterationsNumber);
        Assert.That(res, Is.True);
        return;

        bool Test(int agentsCount, int maxIterations)
        {
            var agents = GetAgents(agentsCount, 1000);
        
            var speciesSettings = new SpeciesSettings(0.5, 0.2, 4, 10, agents.Count);
            var manager = new SpeciesManagerExample(speciesSettings);
            foreach (var agent in agents) manager.Add(agent);
            var settings = new OffspringSettings(0.35, 0.2, 0.25, 0.01);
            var strategy = new OffspringStrategy(settings);
            var genAlg = new NumericGenAlg(manager, FitnessFunctionExample.CountFitnessHimmelblau, strategy);
        
            var bestFitness = GetBestFitness(genAlg, maxIterations);
            return bestFitness < 0.001;
        }
    }

    private List<IAgent> GetAgents(int count, double abs)
    {
        var agents = new List<IAgent>();
        for (var i = 0; i < count; i++)
        {
            var x = abs * (2 * ArrayUtils.SharedRandom.NextDouble() - 1);
            var y = abs * (2 * ArrayUtils.SharedRandom.NextDouble() - 1);
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
