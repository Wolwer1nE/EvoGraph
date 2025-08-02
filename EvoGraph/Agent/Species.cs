using EvoGraph.Epoch;
using EvoGraph.MathUtils;

namespace EvoGraph.Agent;

public abstract class Species(IAgent agent)
{
    /// <summary> Initial agent which represents the species, all new agents are compared only to this one. </summary>
    public IAgent Representative = agent;
    
    /// <summary> List of all members, including representative. </summary>
    public List<IAgent> Members = [agent];

    /// <summary> Average fitness of species on each epoch. </summary>
    public readonly List<double> FitnessHistory = [];
    
    public virtual double MeanFitness()
    {
        var fitness = Members.Sum(x => x.Fitness / Members.Count);
        FitnessHistory.Add(fitness);
        return fitness;
    }

    public virtual void SortByFitness()
    {
        Members.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
        MeanFitness();
    }

    /// <summary> This method is a metric of difference between genes of two agents. </summary>
    protected abstract double Distance(IAgent x, IAgent y);
    
    /// <returns> True if agent has been successfully added to this species, false otherwise. </returns>
    public virtual bool TryToAdd(IAgent agent, double threshold)
    {
        if (Distance(agent, Representative) > threshold) return false;
        Members.Add(agent);
        return true;
    }

    public virtual double Offspring()
    {
        var diversityFactor = 1.0 / Members.Count;
        return 1 / FitnessHistory[^1] * diversityFactor;
    }
}
