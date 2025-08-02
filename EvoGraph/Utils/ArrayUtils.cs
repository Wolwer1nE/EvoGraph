namespace EvoGraph.MathUtils;

public static class ArrayUtils
{
    /// <summary> Provides a Random class instance same for all users. </summary>
    public static Random SharedRandom { set; get; } = new Random();

    public static void SetRandomSeed(int seed)
    {
        SharedRandom = new Random(seed);
    }
    
    /// <summary> Shuffle the array using Fisher-Yates algorithm (modifies source array). </summary>
    public static T[] Shuffle<T>(this T[] array)
    {
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = SharedRandom.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }
    
    /// <returns> An array of [count] unique random positive numbers less than [max]. </returns>
    public static int[] RandomSet(int max, int count)
    {
        var array = new int[max];
        for (var i = 0; i < max; i++) array[i] = i;
        return array.Shuffle()[..count];
    }
    
    /// <returns> A random element from this list. </returns>
    public static T RandomElement<T>(this List<T> array)
    {
        return array[SharedRandom.Next(array.Count)];
    }
    
    /// <returns> An array T[size, size] where all elements are equal the given value. </returns>
    public static T[,] MatrixWithValue<T>(int size, T value)
    {
        var matrix = new T[size, size];
        for (var row = 0;  row < size;  row++)
        for (var col = 0; col < size; col++)
            matrix[row, col] = value;
        return matrix;
    }
    
    /// <summary> Inverses a bit in binary string (should be an array of char). </summary>
    public static char[] InverseBit(this char[] binary, int index)
    {
        if (binary[index] == '0') binary[index] = '1';
        else if (binary[index] == '1') binary[index] = '0';
        else throw new ArgumentException("Not a binary");
        return binary;
    }
}
