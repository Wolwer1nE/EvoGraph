using EvoGraph.Agent;

namespace EvoGraphTest.AgentTest;

public static class FitnessFunctionExample
{
    /// <summary> fitness := x^2 + y^2 </summary>
    public static double CountFitnessParaboloid(IAgent agent)
    {
        var ex = agent as AgentExample ?? throw new ArgumentException("Agent type is not AgentExample");
        
        ex.Fitness = ex.X * ex.X + ex.Y * ex.Y;
        ex.Fitness = double.IsNaN(ex.Fitness) ? double.MaxValue : ex.Fitness;
        return ex.Fitness;
    }
    
    /// <summary> fitness := (1 - x)^2 + 100 * (y - x^2)^2 </summary>
    public static double CountFitnessRosenbrock(IAgent agent)
    {
        var ex = agent as AgentExample ?? throw new ArgumentException("Agent type is not AgentExample");

        double a = 1 - ex.X, b = ex.Y - ex.X * ex.X;
        ex.Fitness = a * a + 100 * b * b;
        ex.Fitness = double.IsNaN(ex.Fitness) ? double.MaxValue : ex.Fitness;
        return ex.Fitness;
    }

    /// <summary> fitness := (x^2 + y - 11)^2 + (x + y^2 - 7)^2 </summary>
    public static double CountFitnessHimmelblau(IAgent agent)
    {
        var ex = agent as AgentExample ?? throw new ArgumentException("Agent type is not AgentExample");

        var a = ex.X * ex.X + ex.Y - 11;
        var b = ex.X + ex.Y * ex.Y - 7;
        ex.Fitness = a * a + b * b;
        ex.Fitness = double.IsNaN(ex.Fitness) ? double.MaxValue : ex.Fitness;
        return ex.Fitness;
    }
}