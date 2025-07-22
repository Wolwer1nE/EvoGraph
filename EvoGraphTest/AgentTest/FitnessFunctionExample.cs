using EvoGraph.Agent;

namespace EvoGraphTest.AgentTest;

public class FitnessFunctionExample
{
    /// <summary>
    /// Min value of <b>x**2 + y**2</b> function
    /// </summary>
    /// <returns><b>
    /// x**2 + y**2
    /// </b></returns>

    public static double CountFitness(IAgent agent)
    {
        AgentExample ex = agent as AgentExample ?? 
                          throw new ArgumentException("Agent type should be AgentExample");
        
        ex.Fitness = ex.X * ex.X + ex.Y * ex.Y ;
        return ex.Fitness;
    }
}