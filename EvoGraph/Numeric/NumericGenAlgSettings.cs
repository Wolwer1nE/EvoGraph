namespace EvoGraph.Numeric;

public class NumericGenAlgSettings
{
    public double MutationPercentage { get; set; }
    public double NaturalSelectionPercentage { get; set; }
    public double TournamentPercentage { get; set; }

    public NumericGenAlgSettings(double mutation = 0.2, double naturalSelection = 0.1, double tournament = 0.2)
    {
        MutationPercentage = mutation;
        NaturalSelectionPercentage = naturalSelection;
        TournamentPercentage = tournament;
    }
}