using EvoGraph.Agent;
using EvoGraph.MathUtils;
using EvoGraph.Random;

namespace EvoGraph.Epoch;

public class Epoch
{
    public List<IAgent> Population;

    public Epoch(List<IAgent> population)
    {
        Population = population;
    }

    /// <summary>
    /// Modifies some random agents from population.
    /// </summary>
    /// <param name="percent">
    /// How many agents should mutate (20% = 0.20).
    /// </param>
    public void Mutation(double percent = 0.2)
    {
        var mask = ArrayUtils.PercentMask(Population.Count, percent);
        foreach (int i in mask)
            Population[i] = Population[i].Mutation();
    }

    /// <summary>
    /// Rank agents (sort by fitness score in decreasing order,
    /// like a <b>"pound for pound"</b> rating).
    /// </summary>
    public void P4P()
    {
        foreach (var agent in Population)
        {
            agent.CountFitness();
        }

        Population.Sort((a, b) =>
        {
            if (a.Fitness > b.Fitness) return -1;
            return a.Fitness < b.Fitness ? 1 : 0;
        });
    }

    /// <summary>
    /// Replace the worst agents with the children of best ones.
    /// Population should be sorted by agents fitness score (first agent is the best).
    /// </summary>
    /// <param name="percent">
    /// How many agents should give a child (10% = 0.10).
    /// </param>
    public void NaturalSelection(double percent = 0.1)
    {
        int count = Population.Count;
        int num = (int)(count * percent);
        var mask = ArrayUtils.PercentMask(Population.Count, percent);
        for (int i = 1; i < num; i++)
        {
            IAgent mother = Population[mask[i]];
            IAgent father = Population[mask[i - 1]];
            Population[count - i] = mother.Crossover(father);
        }
    }

    /// <summary>
    /// Compare random pairs then save the winner and write his copy
    /// to the place of loser (that's the law of jungles).
    /// Works on sorted population.
    /// </summary>
    /// <param name="percent">
    /// How many agents should go to the gladiator tournament (20% = 0.2).
    /// </param>
    public void Tournament(double percent = 0.2)
    {
        var mask = ArrayUtils.PercentMask(Population.Count, percent);
        for (int i = 1; i < mask.Count; i++)
        {
            if (mask[i] < mask[i - 1])
                Population[mask[i - 1]] = Population[mask[i]].Clone();
            else
                Population[mask[i]] = Population[mask[i - 1]].Clone();
        }

    }
}

