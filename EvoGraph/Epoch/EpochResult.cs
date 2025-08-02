namespace EvoGraph.Epoch;

public class EpochResult(int epoch, double bestFitness)
{
    public int EpochNumber { get; set; } = epoch;
    public double BestFitness { get; } = bestFitness;
}
