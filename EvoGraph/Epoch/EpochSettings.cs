namespace EvoGraph.Epoch;

public class EpochSettings
{
    public double MutationPercent { get; set; }
    
    public double CrossoverPercent { get; set; }
    
    public double TournamentPercent { get; set; }
    public double ElitePercent { get; set; }

    public EpochSettings(double mutation, double crossover, double tournament, double elite)
    {
        if (mutation + crossover + tournament > 1)
            throw new ArgumentException("Percentages sum can't be more than 1");
        
        MutationPercent = mutation;
        CrossoverPercent = crossover;
        TournamentPercent = tournament;
        
        ElitePercent = elite;
    }
}
