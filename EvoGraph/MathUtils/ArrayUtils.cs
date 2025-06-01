using EvoGraph.Random;

namespace EvoGraph.MathUtils;

public class ArrayUtils
{
    /// <summary>
    /// Makes a list of random unique  integers
    /// (to form arrays subsets).
    /// </summary>
    /// <param name="count">
    /// Total amount of elements in indexed array.
    /// </param>
    /// <param name="percent">
    /// How big should subset be (20% = 0.2).
    /// </param>
    /// <returns>
    /// List of <b> (count * percent) </b> unique numbers
    /// in <b> [0, count) </b> range.
    /// </returns>
    public static List<int> PercentMask(int count, double percent)
    {
        var num = (int)(count * percent);
        var ans = new List<int>();
        //ans.Add(Rnd.NextInt(count));
        for (int i = 0; i < num; i++)
        {
            int val = Rnd.NextInt(count);
            while (ans.Contains(val))
                val = Rnd.NextInt(count);
            ans.Add(val);
        }

        return ans;
    }
}