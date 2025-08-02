using EvoGraph.Numeric;

namespace EvoGraph.Agent;

public interface ISpeciesManager
{
    /// <summary> Max distance between agents in one species. </summary>
    public double Threshold { get; set; }
    
    public int TargetSpeciesCount { get; set; }
    
    public int MaxSpeciesCount { get; set; }
    
    public int PopulationSize { get; set; }
    
    public List<Species> SpeciesList { get; }
    
    /// <summary> Add an agent to existing species or create a new one. </summary>
    public void Add(IAgent agent);

    /// <summary> Remove bad species which didn't have any improvements for a long time. </summary>
    public void SpeciesCulling();

    /// <summary> Calculate how big should be each species offspring in next step. </summary>
    public List<int> ExpectedOffspring(); // TODO: remember of this 1/fitness, its only for decreasing

    public void SortSpeciesByMeanFitness()
    {
        foreach (var species in SpeciesList) species.SortByFitness();
        SpeciesList.Sort((x, y) => x.FitnessHistory[^1].CompareTo(y.FitnessHistory[^1]));
    }

    /// <summary> For each species delete all members after the best one. </summary>
    public void ClearSpeciesExceptFirst()
    {
        foreach (var species in SpeciesList)
        {
            var best = species.Members.MinBy(a => a.Fitness);
            species.Representative = best ?? species.Members[0];
            species.Members = [species.Representative];
        }
    }
}
