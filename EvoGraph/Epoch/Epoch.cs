using EvoGraph.Agent;
using EvoGraph.MathUtils;

namespace EvoGraph.Epoch;

public class Epoch(EpochSettings settings, ISpeciesManager manager, OffspringStrategy strategy)
{
    public readonly EpochSettings Settings = settings;
    public readonly ISpeciesManager SpeciesManager = manager;
    public readonly OffspringStrategy Strategy = strategy;

    public virtual EpochResult Step(int step)
    {
        SpeciesManager.SortSpeciesByMeanFitness();
        var bestFitness = SpeciesManager.SpeciesList.Min(s => s.Members[0].Fitness);
        
        SpeciesManager.SpeciesCulling();
        
        List<IAgent> offspring = [];
        var populations = SpeciesManager.ExpectedOffspring();
        for (var i = 0; i < SpeciesManager.SpeciesList.Count; ++i)
        {
            var species = SpeciesManager.SpeciesList[i];
            offspring.AddRange(Strategy.GetOffspring(species, populations[i]));
        }
        
        SpeciesManager.ClearSpeciesExceptFirst();
        foreach (var child in offspring) SpeciesManager.Add(child);

        return new EpochResult(step, bestFitness);
    }
}
