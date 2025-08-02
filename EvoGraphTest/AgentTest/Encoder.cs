namespace EvoGraphTest.AgentTest;

public static class Encoder
{
    public static string ToBinaryString(this double x)
    {
        var bits = BitConverter.DoubleToInt64Bits(x);
        return Convert.ToString(bits, 2).PadLeft(64, '0');
    }
    
    public static string ToBinaryString(this int x)
    {
        return Convert.ToString(x, 2).PadLeft(32, '0');
    }

    public static double BinaryToDouble(this string str) // TODO: check for NaN and Infinities
    {
        var newBits = Convert.ToInt64(str, 2);
        return BitConverter.Int64BitsToDouble(newBits);
    }
    
    public static int BinaryToInt32(this string str)
    {
        return Convert.ToInt32(str, 2);
    }
}
