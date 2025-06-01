namespace EvoGraph.Random;

public class Rnd
{
    private static System.Random _singleton = new System.Random();

    public static double NextDouble()
    {
        return _singleton.NextDouble();
    }
    
    public static double NextDouble(double maxValue)
    {
        return maxValue * NextDouble();
    }
    
    public static int NextInt(int maxValue)
    {
        return _singleton.Next(maxValue);
    }
}
