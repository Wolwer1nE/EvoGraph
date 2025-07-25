using EvoGraph.Agent;
using EvoGraph.Random;

namespace EvoGraphTest.NNTest;

public class NeuralAgent: IAgent
{
    public NeuralNetwork Network;
    
    public string Dna {private set; get;}
    
    public double Fitness { set; get; }

    public NeuralAgent(NeuralNetwork network)
    {
        Network = network;
        Dna = network.GetWeights();
        Fitness = Double.MaxValue;
    }
    
    public NeuralAgent(NeuralNetwork network, string dna)
    {
        Dna = dna;
        Network = network;
        Network.SetWeights(dna);
        Fitness = Double.MaxValue;
    }

    public IAgent Clone()
    {
        return new NeuralAgent(Network.Clone())
        {
            Fitness = Fitness
        };
    }

    public IAgent Crossover(IAgent other)
    {
        NeuralAgent? first = other as NeuralAgent;
        if (first == null) return new NeuralAgent(Network.Clone());
        
        var charsThis = Dna.ToCharArray();
        var charsFirst = first.Dna.ToCharArray();
        
        string geneChild = "";
        for (int i = 0; i < Dna.Length; ++i)
        {
            if (Rnd.NextDouble() > 0.5)
                geneChild += charsThis[i];
            else 
                geneChild += charsFirst[i];
        }

        return new NeuralAgent(Network.Clone(), geneChild);
    }

    public IAgent Mutation()
    {
        var chars = Dna.ToCharArray();
        int pos = Rnd.NextInt(Dna.Length);
        if (chars[pos] == '0') chars[pos] = '1';
        else if (chars[pos] == '1') chars[pos] = '0';
        return new NeuralAgent(Network.Clone(), new string(chars));
    }
}