namespace EvoGraph.Agent;

public interface ISpeciesManager
{
    public List<Species> SpeciesList { get; }
    
    /// <summary> Add an agent to existing species or create a new one. </summary>
    public void Add(IAgent agent);

    /// <summary> Remove redundant species. </summary>
    public void SpeciesCulling();

    /// <summary> Calculate how many agents should be in each species offspring. </summary>
    public List<int> ExpectedOffspring();

    /// <summary> Sort species by their mean fitness. </summary>
    public void Sort(bool isAscending = true)
    {
        foreach (var species in SpeciesList) species.Sort(isAscending);
        if (isAscending) SpeciesList.Sort((x, y) => x.MeanFitness().CompareTo(y.MeanFitness()));
        else SpeciesList.Sort((x, y) => y.MeanFitness().CompareTo(x.MeanFitness()));
    }

    /// <summary> Delete all members in species (except the best member which becomes representative). </summary>
    public void ClearSpecies()
    {
        foreach (var species in SpeciesList)
        {
            var best = species.Members.MinBy(a => a.Fitness);
            species.Representative = best ?? species.Members[0];
            species.Members = [species.Representative];
        }
    }
}
