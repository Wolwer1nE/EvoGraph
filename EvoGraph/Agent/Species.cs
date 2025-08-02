namespace EvoGraph.Agent;

// TODO: add flag for sorted members, add flag for calculated fitness

public abstract class Species(IAgent agent)
{
    /// <summary> Initial agent which represents the species, all new agents are compared only to this one. </summary>
    public IAgent Representative = agent;
    
    /// <summary> List of all members, including representative. </summary>
    public List<IAgent> Members = [agent];
    
    protected double Fitness = double.MaxValue;
    
    /// <summary> Returns cashed mean fitness or recalculates it. </summary>
    public double MeanFitness(bool recalculate = false)
    {
        if (recalculate) Fitness = Members.Sum(x => x.Fitness / Members.Count);
        return Fitness;
    }

    /// <summary> Sort members by fitness in ascending or descending order. </summary>
    public virtual void Sort(bool isAscending = true)
    {
        MeanFitness(true);
        if (isAscending) Members.Sort((x, y) => x.Fitness.CompareTo(y.Fitness));
        else Members.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
    }
    
    /// <returns> True if agent has been successfully added to this species, false otherwise. </returns>
    public virtual bool TryToAdd(IAgent agent, double threshold)
    {
        if (Distance(agent, Representative) > threshold) return false;
        Members.Add(agent);
        return true;
    }
    
    /// <summary> This method is a metric of difference between genes of two agents. </summary>
    protected abstract double Distance(IAgent x, IAgent y);
    
    /// <summary> The greater the returned value, the larger offspring species will have. </summary>
    public abstract double OffspringFactor();
}
