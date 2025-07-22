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

    /// <summary>
    /// Make a new instance of squared [size, size] matrix with the all elements
    /// equal to the given <b>value.</b>
    /// </summary>
    public static double[,] MatrixWithValue(int size, double value)
    {
        double[,] matrix = new double[size, size];
        for (int row = 0;  row < size;  row++)
        for (int col = 0; col < size; col++)
            matrix[ row, col] = value;
        return matrix;
    }
}