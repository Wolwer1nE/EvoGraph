using System.ComponentModel.DataAnnotations;
using EvoGraph.Agent;
using EvoGraph.MathUtils;
using NUnit.Framework.Constraints;

namespace EvoGraphTest.AgentTest;

public class AgentExample : IAgent
{
    public double X { get;}
    public double Y { get;}
    
    public string Gene { get;}
    
    public double Fitness { set; get; }

    public static Random Rnd { set; get; } = new Random();

    public AgentExample(double x, double y)
    {
        X = x;
        Y = y;
        Fitness = double.MaxValue;
        Gene = x.ToBinaryString() + y.ToBinaryString();
    }
    
    public AgentExample(string gene)
    {
        Gene = gene;
        X = gene[..64].BinaryToDouble();
        Y = gene[64..].BinaryToDouble();
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
        if (other is not AgentExample first) return new AgentExample(X, Y);
        
        var charsThis = Gene.ToCharArray();
        var charsFirst = first.Gene.ToCharArray();
        
        string geneChild = "";
        for (int i = 0; i < 128; ++i)
        {
            if (Rnd.NextDouble() > 0.5)
                geneChild += charsThis[i];
            else 
                geneChild += charsFirst[i];
        }
        
        /*
         var gene0 = Gene;
        var gene1 = first.Gene;
        if (Rnd.NextDouble() > 0.5) (gene0, gene1) = (gene1, gene0);
        var geneChild = gene0[..64] + gene1[64..];
         */
        
        return new AgentExample(geneChild);
    }

    public IAgent Mutation()
    {
        var type = (int)(Rnd.NextDouble() * 3);
        return type switch
        {
            0 => new AgentExample(Mutate1()),
            1 => new AgentExample(Mutate2()),
            2 => new AgentExample(Mutate3()),
            _ => Clone()
        };
    }
    
    private string Mutate1()
    {
        var chars = Gene.ToCharArray();
        var pos = Rnd.Next(Gene.Length);
        if (chars[pos] == '0') chars[pos] = '1';
        else if (chars[pos] == '1') chars[pos] = '0';
        return new string(chars);
    }
    
    private string Mutate2()
    {
        var chars = Gene.ToCharArray();
        var pos0 = Rnd.Next(64);
        if (chars[pos0] == '0') chars[pos0] = '1';
        else if (chars[pos0] == '1') chars[pos0] = '0';
        var pos1 = 64 + Rnd.Next(64);
        if (chars[pos1] == '0') chars[pos1] = '1';
        else if (chars[pos1] == '1') chars[pos1] = '0';
        return new string(chars);
    }

    private string Mutate3()
    {
        var x = Rnd.NextDouble() * X;
        var y = Rnd.NextDouble() * Y;
        return x.ToBinaryString() + y.ToBinaryString();
    }
}
