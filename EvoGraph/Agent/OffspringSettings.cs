namespace EvoGraph.Agent;

public class OffspringSettings
{
    /// <summary> How many species members s will mutate. </summary>
    public double MutationPercent { get; set; }
    
    /// <summary> How many couples in species will mutate. </summary>
    public double CrossoverPercent { get; set; }
    
    /// <summary> How many bad members will be replaced with clones of fitter ones. </summary>
    public double TournamentPercent { get; set; }
    
    /// <summary> How many members will go to the next generation without changes. </summary>
    public double ElitePercent { get; set; }

    public OffspringSettings(double mutation, double crossover, double tournament, double elite)
    {
        if (mutation + crossover + tournament > 1)
            throw new ArgumentException("Percentages sum can't be more than 1");
        
        MutationPercent = mutation;
        CrossoverPercent = crossover;
        TournamentPercent = tournament;
        
        ElitePercent = elite;
    }
}
