namespace EvoGraph.Agent;

public interface IAgent
{
    /// <summary> Get stored fitness function value (won't affect the value). </summary>
    public double Fitness { get; }

    /// <summary> Deep clone of this agent instance. </summary>
    public IAgent Clone();

    /// <summary> Make a hybrid of two agents (won't affect original agents). </summary>
    /// <returns> A new instance of hybridized agent. </returns>
    public IAgent Crossover(IAgent other);
    
    /// <summary> Randomly modifies agent params (won't affect original agent). </summary>
    /// <returns> A new instance of modified agent. </returns>
    public IAgent Mutation();
}
