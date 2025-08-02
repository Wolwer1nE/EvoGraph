using EvoGraph.Agent;
using EvoGraph.Epoch;

namespace EvoGraph.Numeric;

public delegate double FitnessFunction(IAgent agent);

public class NumericGenAlg(ISpeciesManager manager, FitnessFunction ff, OffspringStrategy strategy) : IGenAlg
{
    private int _step;
    
    private Epoch.Epoch _epoch = new(manager, strategy);

    public void CountFitness()
    {
        var agents = _epoch.SpeciesManager.SpeciesList.SelectMany(species => species.Members);
        foreach (var agent in agents) ff(agent);
    }
    
    public EpochResult Step()
    {
        CountFitness();
        return _epoch.Step(_step++);
    }
}
