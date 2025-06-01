using EvoGraph.Agent;

namespace EvoGraph.Numeric;

public class NumericGenAlg : IGenAlg
{
    private int _step;
    
    private Epoch.Epoch _epoch;

    public NumericGenAlg(List<IAgent> agents)
    {
        _step = 0;
        _epoch = new Epoch.Epoch(agents);
    }
    
    public EpochResult Step()
    {
        ++_step;
        _epoch.Mutation();
        _epoch.P4P();
        _epoch.Tournament();
        _epoch.NaturalSelection();
        return new EpochResult(_step, _epoch.Population[0].Fitness);
    }
}