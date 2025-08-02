namespace EvoGraph.Agent;

public class SpeciesSettings
{
    /// <summary> Maximal distance between member and representative in species. </summary>
    public double Threshold { get; set; }
    
    /// <summary> Strength of the threshold changes to achieve the target species count (negative feedback). </summary>
    public double ThresholdFactor { get; set; }
    
    public int TargetSpeciesCount { get; set; }
    
    /// <summary> If species number increase this value, the worst species will be deleted. </summary>
    public int MaxSpeciesCount { get; set; }
    
    /// <summary> Total amount of all members from all species. </summary>
    public int PopulationSize { get; set; }

    /// <summary> SpeciesSettings for one unique species (no species, all agents are the same one). </summary>
    public SpeciesSettings(int population)
    {
        Threshold = double.MaxValue;
        TargetSpeciesCount = -1;
        MaxSpeciesCount = 1;
        PopulationSize = population;
    }
    
    public SpeciesSettings(double threshold, double factor, int targetSpecies, int maxSpecies, int population)
    {
        Threshold = threshold;
        ThresholdFactor = factor;
        TargetSpeciesCount = targetSpecies;
        MaxSpeciesCount = maxSpecies;
        PopulationSize = population;
    }
}