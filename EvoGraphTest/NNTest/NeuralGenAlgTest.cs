using EvoGraph.Agent;
using EvoGraph.Numeric;

namespace EvoGraphTest.NNTest;

public class NeuralGenAlgTest
{
    private List<IAgent> GetAgents(int count, int[] layers, string[] activations)
    {
        var agents = new List<IAgent>();
        for (int i = 0; i < count; i++)
        {
            var network = new NeuralNetwork(layers, activations);
            network.SetRandom();
            agents.Add(new NeuralAgent(network));
        }

        return agents;
    }
    
    [Test]
    public void BasicNumericGenAlgTest()
    {
        int agentsCount = 10000;
        var layers = new int[] { 3, 1 };
        var activations = new string[] { "relu" };
        var agents = GetAgents(agentsCount, layers, activations);

        var ff = new FitnessExample();
        var settings = new NumericGenAlgSettings();
        var genAlg = new NumericGenAlg(agents, ff.CountFitness, settings);

        int epochCount = 1000;
        var bestFitness = Double.MaxValue;
        for (int i = 0; i < epochCount; i++)
        {
            bestFitness = genAlg.Step().BestFitness;
            if (i % 50 == 0) TestContext.Progress.WriteLine("Step: " + i + "; Best Fitness: " + bestFitness);
            if (bestFitness == 0) break;
        }

        string dna = (agents[0] as NeuralAgent).Dna;
        var ans = Encoder.Encoder.DecodeArray(dna);
        foreach (var a in ans)
            Console.WriteLine(a);
        //Assert.That(bestFitness < 0.001);
    }
}