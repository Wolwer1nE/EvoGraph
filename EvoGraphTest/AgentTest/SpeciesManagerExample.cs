using EvoGraph.Agent;

namespace EvoGraphTest.AgentTest;

public class SpeciesManagerExample : ISpeciesManager
{
    public double Threshold { get; set; }
    
    public double ThresholdFactor { get; set; }
    public int TargetSpeciesCount { get; set; }
    public int MaxSpeciesCount { get; set; }
    public int PopulationSize { get; set; }
    public List<Species> SpeciesList { get; }

    private SpeciesManagerExample(int populationSize)
    {
        Threshold = double.MaxValue;
        TargetSpeciesCount = -1;
        PopulationSize = populationSize;
        SpeciesList = [];
    }

    public static SpeciesManagerExample NoSpeciesManager(int populationSize) => new (populationSize);
    
    public SpeciesManagerExample(double threshold, double thresholdFactor, int targetSpeciesCount, int maxSpeciesCount,
        int populationSize, int k)
    {
        Threshold = threshold;
        ThresholdFactor = thresholdFactor;
        TargetSpeciesCount = targetSpeciesCount;
        MaxSpeciesCount = maxSpeciesCount;
        PopulationSize = populationSize;
        K = k;
        SpeciesList = [];
    }
    
    public void Add(IAgent agent)
    {
        if (agent is not AgentExample) throw new ArgumentException("Agent is not AgentExample");
        if (!SpeciesList.Aggregate(false, Aggregator)) SpeciesList.Add(new SpeciesExample(agent));
        return;
        
        bool Aggregator(bool current, Species species) => current | species.TryToAdd(agent, Threshold);
    }

    public int K;
    
    public void SpeciesCulling()
    {
        // if (SpeciesList.Count < TargetSpeciesCount)
        // {
        //     Threshold -= ThresholdFactor * Threshold;
        //     return;
        // }
        // if (SpeciesList.Count != TargetSpeciesCount) Threshold += ThresholdFactor * Threshold;
        if (TargetSpeciesCount < 0) return;
        var range = SpeciesList.Count - MaxSpeciesCount;
        if (range > 0) SpeciesList.RemoveRange(MaxSpeciesCount, range - 1);
    }
    
    public List<int> ExpectedOffspring() // TODO: remember of this 1/fitness, its only for decreasing
    {
        if (TargetSpeciesCount < 0) return [PopulationSize];
        var sum = SpeciesList.Sum(s => s.Offspring());
        return SpeciesList.Select(Offspring).ToList();

        int Offspring(Species s) => (int)(s.Offspring() / sum * PopulationSize);
    }
}