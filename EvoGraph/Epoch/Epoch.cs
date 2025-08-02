using EvoGraph.Agent;

namespace EvoGraph.Epoch;

public class Epoch(ISpeciesManager manager, OffspringStrategy strategy)
{
    public readonly ISpeciesManager SpeciesManager = manager;
    public readonly OffspringStrategy Strategy = strategy;

    public virtual EpochResult Step(int step)
    {
        SpeciesManager.Sort();
        SpeciesManager.SpeciesCulling();
        
        var bestFitness = SpeciesManager.SpeciesList.Min(s => s.Members[0].Fitness);
        
        List<IAgent> offspring = [];
        var populations = SpeciesManager.ExpectedOffspring();
        for (var i = 0; i < SpeciesManager.SpeciesList.Count; ++i)
        {
            var species = SpeciesManager.SpeciesList[i];
            offspring.AddRange(Strategy.GetOffspring(species, populations[i]));
        }
        
        SpeciesManager.ClearSpecies();
        foreach (var child in offspring) SpeciesManager.Add(child);

        return new EpochResult(step, bestFitness);
    }
}
