using EvoGraph.Agent;

namespace EvoGraph.Numeric;

public delegate double FitnessFunction(IAgent agent);

public class NumericGenAlg : IGenAlg
{
    private int _step;
    
    private Epoch.Epoch _epoch;

    private FitnessFunction FitnessFunc { set; get; }

    public NumericGenAlgSettings Settings { set; get; }

    public NumericGenAlg(List<IAgent> agents, FitnessFunction ff, NumericGenAlgSettings settings)
    {
        _step = 0;
        _epoch = new Epoch.Epoch(agents);
        FitnessFunc = ff;
        Settings = settings;
    }
    
    public void CountFitnesses()
    {
        foreach (var agent in _epoch.Population)
        {
            FitnessFunc(agent);
        }
    }
    
    public EpochResult Step()
    {
        ++_step;
        
        CountFitnesses();
        
        _epoch.P4P();
        _epoch.Mutation(Settings.MutationPercentage);
        _epoch.Tournament(Settings.TournamentPercentage);
        _epoch.NaturalSelection(Settings.NaturalSelectionPercentage);
        _epoch.P4P();
        return new EpochResult(_step, _epoch.Population[0].Fitness);
    }
}