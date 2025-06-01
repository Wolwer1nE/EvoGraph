namespace EvoGraph.Agent;

public interface IAgent
{
    /// <summary>
    /// Get the fitness functions value
    /// </summary>
    public double Fitness { get; }

    /// <summary>
    /// Copy an instance of this agent
    /// </summary>
    public IAgent Clone();
    
    /// <summary>
    /// Count the fitness function value
    /// </summary>
    public double CountFitness();

    /// <summary>
    /// Make a hybrid of two agents (makes a <b>new</b> instance,
    /// doesn't change the original agent)
    /// </summary>
    public IAgent Crossover(IAgent other);
    
    /// <summary>
    /// Randomly modifies agent params (makes a <b>new</b> instance,
    /// doesn't change the original agent)
    /// </summary>
    public IAgent Mutation();
}