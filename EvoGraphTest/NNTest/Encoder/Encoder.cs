using EvoGraph.Random;

namespace EvoGraphTest.NNTest.Encoder;

public static class Encoder
{
    public static string EncodeDouble(double x)
    {
        long bits = BitConverter.DoubleToInt64Bits(x);
        return Convert.ToString(bits, 2).PadLeft(64, '0');
    }

    public static string EncodeArray(double[] values)
    {
        string ans = "";
        for (int i = 0; i < values.Length; i++)
            ans += EncodeDouble(values[i]);
        return ans;
    }
    
    public static double DecodeDouble(string str)
    {
        long newBits = Convert.ToInt64(str, 2);
        return BitConverter.Int64BitsToDouble(newBits);
    }

    public static double[] DecodeArray(string str)
    {
        double[] ans = new double[str.Length / 64];
        for (int i = 0; i < str.Length; i += 64)
            ans[i / 64] = DecodeDouble(str.Substring(i, 64));
        return ans;
    }

    [Test]
    public static void Test()
    {
        double[] array = new double[10];
        for (int i = 0; i < 10; i++)
            array[i] = Rnd.NextDouble();
        
        double[] decoded = DecodeArray(EncodeArray(array));
        Assert.That(decoded, Is.EqualTo(array));
    }
}