﻿using EvoGraph.Agent;

namespace EvoGraphTest.AgentTest;

public class SpeciesExample(IAgent agent) : Species(agent)
{
    /// <summary> Discrete metric based on the number of decimal digits. </summary>
    protected override double Distance(IAgent x, IAgent y)
    {
        var a = x as AgentExample ?? throw new ArgumentException("x is not AgentExample");
        var b = y as AgentExample ?? throw new ArgumentException("y is not AgentExample");
        
        var absA = Math.Sqrt(a.X * a.X + a.Y * a.Y);
        var absB = Math.Sqrt(b.X * b.X + b.Y * b.Y);
        if (double.IsNaN(absA) || double.IsNaN(absB)) return 1;
        
        if (absA <= 1 && absB <= 1) return 0;
        if (absA is >= 1 and <= 10 && absB is >= 1 and <= 10) return 0;
        if (absA is >= 10 and <= 1000 && absB is >= 10 and <= 1000) return 0;
        if (absA >= 1000 && absB >= 1000) return 0;
        return 1;
    }

    /// <summary> The less fitness, the greater offspring.
    /// The more agents, the less offspring (to avoid dominance of one species). </summary>
    public override double OffspringFactor()
    {
        var diversityFactor = 1.0 / Members.Count;
        return 1.0 / Fitness * diversityFactor;
    }
}