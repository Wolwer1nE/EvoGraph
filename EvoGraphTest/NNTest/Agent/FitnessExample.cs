using EvoGraph.Agent;
using EvoGraph.Random;

namespace EvoGraphTest.NNTest;

public class FitnessExample
{
    public double CountFitness(IAgent agent)
    {
        NeuralAgent ai = agent as NeuralAgent ??
                         throw new ArgumentException("Agent type should be NeuralAgent");

        int count = 1000;

        double mse = 0;
        for (int i = 0; i < count; i++)
        {
            var x = new[] { Rnd.NextDouble(), Rnd.NextDouble(), Rnd.NextDouble() };
            double y = (x[0] + x[1] - x[2]) / 3;
            
            double ans = ai.Network.Forward(x)[0];
            mse += (ans - y) * (ans - y);
        }

        ai.Fitness = mse / count;
        return ai.Fitness;
    }
}