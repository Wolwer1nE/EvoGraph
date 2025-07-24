using EvoGraph.Agent;
using EvoGraph.Random;
using NUnit.Framework.Constraints;

namespace EvoGraphTest.AgentTest;

public class AgentExample : IAgent
{
    public double X {private set; get;}
    public double Y {private set; get;}
    
    public string Gene {private set; get;}
    
    public double Fitness { set; get; }
    
    public string Encode()
    {
        long bitsX = BitConverter.DoubleToInt64Bits(X);
        string binaryX = Convert.ToString(bitsX, 2).PadLeft(64, '0');
        long bitsY = BitConverter.DoubleToInt64Bits(X);
        string binaryY = Convert.ToString(bitsY, 2).PadLeft(64, '0');
        
        return binaryX + binaryY;
    }

    public (double x, double y) Decode()
    {
        long newBitsX = Convert.ToInt64(Gene.Substring(0, 64), 2);
        double x = BitConverter.Int64BitsToDouble(newBitsX);
        
        long newBitsY = Convert.ToInt64(Gene.Substring(64, 64), 2);
        double y = BitConverter.Int64BitsToDouble(newBitsY);
        
        return (x, y);
    }


    public AgentExample(double x, double y)
    {
        X = x;
        Y = y;
        Fitness = Int32.MaxValue;
        Gene = Encode();
    }
    
    public AgentExample(string gene)
    {
        Gene = gene;
        (X, Y) = Decode();
        Fitness = Int32.MaxValue;
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
        AgentExample? first = other as AgentExample;
        if (first == null) return new AgentExample(X, Y);
        
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

        return new AgentExample(geneChild);
    }

    public IAgent Mutation()
    {
        var chars = Gene.ToCharArray();
        int pos = Rnd.NextInt(Gene.Length);
        if (chars[pos] == '0') chars[pos] = '1';
        else if (chars[pos] == '1') chars[pos] = '0';
        return new AgentExample(new string(chars));
    }
}
