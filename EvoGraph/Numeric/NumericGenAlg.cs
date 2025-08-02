using EvoGraph.Agent;
using EvoGraph.Epoch;

namespace EvoGraph.Numeric;

public delegate double FitnessFunction(IAgent agent);

public class NumericGenAlg : IGenAlg
{
    private int _step;
    
    private Epoch.Epoch _epoch;

    private readonly FitnessFunction _ff;

    public NumericGenAlg(ISpeciesManager manager, FitnessFunction ff, EpochSettings settings, OffspringStrategy strategy)
    {
        _step = 0;
        _epoch = new Epoch.Epoch(settings, manager, strategy);
        _ff = ff;
    }
    
    public void CountFitness()
    {
        var agents = _epoch.SpeciesManager.SpeciesList.SelectMany(species => species.Members);
        foreach (var agent in agents) _ff(agent);
    }
    
    public EpochResult Step()
    {
        CountFitness();
        return _epoch.Step(_step++);
    }
}
