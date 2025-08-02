namespace EvoGraph.MathUtils;

public static class ArrayUtils
{
    public static Random Rnd { set; get; } = new Random();
    
    /// <summary> Shuffle the array using Fisher-Yates algorithm (modifies source array). </summary>
    public static T[] Shuffle<T>(this T[] array)
    {
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = Rnd.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }
    
    /// <returns> A list of [max] x [percent] unique random numbers in range from 0 to [max]. </returns>
    public static int[] PercentMask(int max, double percent)
    {
        var array = new int[max];
        for (var i = 0; i < max; i++) array[i] = i;
        var num = (int)(max * percent);
        return array.Shuffle()[..num];
    }
    
    /// <returns> A list of [count] unique random positive numbers less than [max]. </returns>
    public static int[] PercentMask(int max, int count)
    {
        var array = new int[max];
        for (var i = 0; i < max; i++) array[i] = i;
        return array.Shuffle()[..count];
    }
    
    public static int[] RandomIntArray(int max, int count)
    {
        var array = new int[count];
        for (var i = 0; i < count; i++) array[i] = Rnd.Next(max);
        return array;
    }

    public static T RandomElement<T>(this List<T> array)
    {
        return array[Rnd.Next(array.Count)];
    }

    // TODO: remove as deprecated (there is the same method for T[,])
    public static double[,] MatrixWithValue(int size, double value)
    {
        var matrix = new double[size, size];
        for (var row = 0;  row < size;  row++)
        for (var col = 0; col < size; col++)
            matrix[row, col] = value;
        return matrix;
    }
    
    /// <returns> An array T[size, size] where all elements are equal the given value. </returns>
    private static T[,] MatrixWithValue<T>(int size, T value)
    {
        var matrix = new T[size, size];
        for (var row = 0;  row < size;  row++)
        for (var col = 0; col < size; col++)
            matrix[row, col] = value;
        return matrix;
    }
}
