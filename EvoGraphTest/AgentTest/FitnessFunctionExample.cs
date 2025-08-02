using EvoGraph.Agent;

namespace EvoGraphTest.AgentTest;

public static class FitnessFunctionExample
{
    /// <summary>
    /// Min value of <b>x**2 + y**2</b> function
    /// </summary>
    /// <returns><b>
    /// x**2 + y**2
    /// </b></returns>

    public static double CountFitnessParaboloid(IAgent agent)
    {
        AgentExample ex = agent as AgentExample ?? 
                          throw new ArgumentException("Agent type should be AgentExample");
        
        ex.Fitness = ex.X * ex.X + ex.Y * ex.Y;
        return ex.Fitness;
    }
    
    public static double CountFitnessRosenbrock(IAgent agent)
    {
        var ex = agent as AgentExample ?? throw new ArgumentException("Agent type should be AgentExample");

        double a = 1 - ex.X, b = ex.Y - ex.X * ex.X;
        ex.Fitness = a * a + 100 * b * b;
        ex.Fitness = double.IsNaN(ex.Fitness) ? double.MaxValue : ex.Fitness;
        return ex.Fitness;
    }

    public static double CountFitnessHimmelblau(IAgent agent)
    {
        var ex = agent as AgentExample ?? throw new ArgumentException("Agent type should be AgentExample");

        var a = ex.X * ex.X + ex.Y - 11;
        var b = ex.X + ex.Y * ex.Y - 7;
        ex.Fitness = a * a + b * b;
        ex.Fitness = double.IsNaN(ex.Fitness) ? double.MaxValue : ex.Fitness;
        return ex.Fitness;
    }
}