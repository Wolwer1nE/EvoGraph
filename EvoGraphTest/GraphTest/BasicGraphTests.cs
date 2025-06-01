using EvoGraph.Graph;

namespace EvoGraphTest.GraphTest;

public class BasicGraphTests
{
    
    private static double[,] _answerMatrix = new double[,]
    {
        {-1, 2, -1},
        {2, 5, 4},
        {8, -1, -1}
    };

    private static string _answerString = "3\n-1 2 -1 \n2 5 4 \n8 -1 -1 \n";

    private Graph GenerateGraph()
    {
        var graph = new Graph(2);
        graph.AddEdge(0, 0, -1);
        graph.AddEdge(1, 1, 5);
        graph.AddNode();
        graph.AddEdge(0, 1, 2);
        graph.AddEdge(0, 2, -1);
        graph.AddEdge(1, 0, 2);
        graph.AddEdge(1, 2, 4);
        graph.AddEdge(2, 0, 8);
        graph.AddEdge(2, 1, -1);
        graph.AddEdge(2, 2, -1);
        return graph;
    }
    
    [Test]
    public void GraphCreationTest()
    {
        var graph = GenerateGraph();
        Assert.That(graph.AdjacencyMatrix, Is.EqualTo(_answerMatrix));
    }

    [Test]
    public void SerializationTest()
    {
        var graph = GenerateGraph();
        var str = graph.ToString();
        Assert.That(str, Is.EqualTo(_answerString));
    }
    
    [Test]
    public void DeserializationTest()
    {
        var graph = Graph.FromString(_answerString.Split('\n'));
        Assert.That(graph.AdjacencyMatrix, Is.EqualTo(_answerMatrix));
    }

}