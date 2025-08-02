using EvoGraph.MathUtils;

namespace EvoGraphTest.AgentTest;

public static class Encoder
{
    public static string ToBinaryString(this double x)
    {
        var bits = BitConverter.DoubleToInt64Bits(x);
        return Convert.ToString(bits, 2).PadLeft(64, '0');
    }

    // TODO: Do other handle for NaN and Infinities
    public static double BinaryToDouble(this string str) 
    {
        var newBits = Convert.ToInt64(str, 2);
        var result = BitConverter.Int64BitsToDouble(newBits);
        if (double.IsInfinity(result) || double.IsNaN(result)) 
            return ArrayUtils.SharedRandom.NextDouble();
        return result;
    }
}
