using EvoGraph.Agent;

namespace EvoGraphTest.AgentTest;

public class SpeciesManagerExample : ISpeciesManager
{
    public SpeciesSettings Settings;
    
    public List<Species> SpeciesList { get; }
    
    public SpeciesManagerExample(SpeciesSettings settings)
    {
        Settings = settings;
        SpeciesList = [];
    }
    
    public void Add(IAgent agent)
    {
        if (agent is not AgentExample) throw new ArgumentException("Agent is not AgentExample");
        if (!SpeciesList.Aggregate(false, Aggregator)) SpeciesList.Add(new SpeciesExample(agent));
        return;
        
        bool Aggregator(bool current, Species species) => current | species.TryToAdd(agent, Settings.Threshold);
    }
    
    public void SpeciesCulling()
    {
        if (Settings.TargetSpeciesCount < 0) return; // check for monospecies settings
        var range = SpeciesList.Count - Settings.MaxSpeciesCount;
        if (range > 0) SpeciesList.RemoveRange(Settings.MaxSpeciesCount, range - 1);
    }
    
    public List<int> ExpectedOffspring()
    {
        if (Settings.TargetSpeciesCount < 0) return [Settings.PopulationSize]; // check for monospecies settings
        var sum = SpeciesList.Sum(s => s.OffspringFactor());
        return SpeciesList.Select(Offspring).ToList();

        int Offspring(Species s) => (int)(s.OffspringFactor() / sum * Settings.PopulationSize);
    }
}