using EvoGraph.Agent;
using EvoGraph.MathUtils;

namespace EvoGraphTest.AgentTest;

public class AgentExample : IAgent
{
    public double X { get; }
    
    public double Y { get; }
    
    public string Dna { get; }
    
    public double Fitness { set; get; }

    public AgentExample(double x, double y)
    {
        X = x;
        Y = y;
        Fitness = double.MaxValue;
        Dna = x.ToBinaryString() + y.ToBinaryString();
    }
    
    public AgentExample(string dna)
    {
        Dna = dna;
        X = dna[..64].BinaryToDouble();
        Y = dna[64..].BinaryToDouble();
        Fitness = double.MaxValue;
    }

    public IAgent Clone()
    {
        return new AgentExample(X, Y)
        {
            Fitness = Fitness
        };
    }

    public IAgent Crossover(IAgent other)
    {
        if (other is not AgentExample agent) return new AgentExample(X, Y);
        
        var chars0 = Dna.ToCharArray();
        var chars1 = agent.Dna.ToCharArray();
        
        var geneChild = "";
        for (int i = 0; i < 128; ++i)
        {
            if (ArrayUtils.SharedRandom.NextDouble() > 0.5) geneChild += chars0[i];
            else geneChild += chars1[i];
        }
        
        return new AgentExample(geneChild);
    }

    public IAgent Mutation()
    {
        var type = (int)(ArrayUtils.SharedRandom.NextDouble() * 5);
        return type switch
        {
            < 2 => new AgentExample(MutateOneGene()),
            < 4 => new AgentExample(MutateTwoGenes()),
            >= 4 => new AgentExample(WeakMutation())
        };
    }
    
    private string MutateOneGene()
    {
        var index = ArrayUtils.SharedRandom.Next(64);
        return new string(Dna.ToCharArray().InverseBit(index));
    }
    
    private string MutateTwoGenes()
    {
        var i = ArrayUtils.SharedRandom.Next(64);
        var j = 64 + ArrayUtils.SharedRandom.Next(64);
        return new string(Dna.ToCharArray().InverseBit(i).InverseBit(j));
    }

    private string WeakMutation()
    {
        var x = ArrayUtils.SharedRandom.NextDouble() * X;
        var y = ArrayUtils.SharedRandom.NextDouble() * Y;
        return x.ToBinaryString() + y.ToBinaryString();
    }
}
