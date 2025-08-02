using EvoGraph.Epoch;
using EvoGraph.MathUtils;

namespace EvoGraph.Agent;

public class OffspringStrategy(EpochSettings settings)
{
    public  EpochSettings Settings = settings;
    
    protected virtual void Mutate(List<IAgent> offspring, Species species, int count)
    {
        var mask = ArrayUtils.PercentMask(offspring.Count, count);
        foreach (var i in mask) offspring[i] = offspring[i].Mutation();
    }

    protected virtual void Crossover(List<IAgent> offspring, Species species, int count)
    {
        var mask = ArrayUtils.PercentMask(offspring.Count, count);
        for (var i = 1; i < count; i++)
        {
            var mother = species.Members[mask[i]];
            var father = species.Members[mask[i - 1]];
            offspring[^i] = mother.Crossover(father);
        }
    }

    protected virtual void Tournament(List<IAgent> offspring, Species species, int count)
    {
        var mask = ArrayUtils.RandomIntArray(species.Members.Count, count);
        for (var i = 1; i < count; i++)
        {
            var max = Math.Max(mask[i], mask[i - 1]);
            var min = Math.Min(mask[i], mask[i - 1]);
            offspring[max] = offspring[min];
        }
    }
    
    public virtual List<IAgent> GetOffspring(Species species, int count)
    {
        if (count == 0) return [];
        if (count == 1) return [species.Members[0]];
        
        var offspring = species.Members.Select(a => a.Clone()).ToList();
        var eliteCount = (int)Math.Max(Math.Min(count, offspring.Count) * Settings.ElitePercent, 1);
        var elite = offspring[..eliteCount].Select(a => a.Clone()).ToList();
        
        var mutationCount = (int)(offspring.Count * Settings.MutationPercent);
        var crossoverCount = (int)(offspring.Count * Settings.CrossoverPercent);
        var tournamentCount = (int)(offspring.Count * Settings.TournamentPercent);
        
        Mutate(offspring, species, mutationCount);
        Crossover(offspring, species, crossoverCount);
        Tournament(offspring, species, tournamentCount);
        
        offspring.InsertRange(0, elite);
        
        while (offspring.Count > count) offspring.RemoveAt(ArrayUtils.Rnd.Next(eliteCount, offspring.Count - 1));
        while (offspring.Count < count) offspring.Add(offspring.RandomElement());
        return offspring;
    }
}