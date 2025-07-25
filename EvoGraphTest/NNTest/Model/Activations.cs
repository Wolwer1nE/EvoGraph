namespace EvoGraphTest.NNTest;

public delegate void FuncAct(double[] input, int first, int last);

public static class Activations
{
    public static FuncAct FromString(string str)
    {
        return str switch
        {
            "relu" => Relu,
            "sigmoid" => Sigmoid,
            _ => (_, _, _) => {} 
        };
    }
    
    private static void Relu(double[] x, int first, int last)
    {
        for (int i = first; i < last; i++) 
            x[i] = Math.Max(x[i], 0);
    }
    
    private static void Sigmoid(double[] x, int first, int last)
    {
        for (int i = first; i < last; i++)
            x[i] = 1 / (1 + Math.Exp(-x[i]));
    }

    [Test]
    public static void Test()
    {
        double[] exampleArray = new double[10];
        double[] answerArray = new double[10];
        for (int i = 0; i < 10; i++)
        {
            exampleArray[i] = i;
            answerArray[i] = i is < 5 or > 8 ? i : 1 / (1 + Math.Exp(-i));
        }
        
        var act = FromString("sigmoid");
        act(exampleArray, 5, 9);
        //Console.WriteLine(exampleArray);
        
        Assert.That(exampleArray, Is.EqualTo(answerArray));
    }
}