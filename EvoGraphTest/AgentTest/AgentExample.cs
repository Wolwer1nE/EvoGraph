using EvoGraph.Agent;
using EvoGraph.Random;

namespace EvoGraphTest.AgentTest;

public class AgentExample : IAgent
{
    private double X {set; get;}
    private double Y {set; get;}
    
    public double Fitness { get; set; }

    public AgentExample(double x, double y)
    {
        X = x;
        Y = y;
        Fitness = -1;
    }

    public IAgent Clone()
    {
        return new AgentExample(X, Y)
        {
            Fitness = Fitness
        };
    }
    
    /// <summary>
    /// Min value of <b>x**2 + y**2</b> function
    /// </summary>
    /// <returns><b>
    /// 1 / (x**2 + y**2)
    /// </b></returns>

    public double CountFitness()
    {
        var fit = X * X + Y * Y ;
        Fitness = 1 / (fit + Double.Epsilon);
        return Fitness;
    }

    public IAgent Crossover(IAgent other)
    {
        AgentExample? first = other as AgentExample;
        if (first == null) return new AgentExample(X, Y);

        var a = Rnd.NextDouble() + 0.1;
        var b = Rnd.NextDouble() + 0.1;
        var c = Rnd.NextDouble() + 0.1;
        var d = Rnd.NextDouble() + 0.1;
        
        var x = (X * a + first.X * b) / (a + b);
        var y = (Y * c + first.Y * d) / (c + d);
        return new AgentExample(x, y);
    }

    public IAgent Mutation()
    {
        var shift = 0.01 * (0.5 - Rnd.NextDouble());
        var x = shift + X;
        var y = shift + Y;
        return new AgentExample(x, y);
    }
}
