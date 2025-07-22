using EvoGraph.Graph;

namespace EvoGraphTest.GraphTest.GraphAlgorithmsTest;

public class GraphConnectivityTests
{
    [Test]
    public static void TestConnectivityComponents_SingleComponent()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 nodes
        graph.AdjacencyMatrix[0,1] = 1; // Edge between 0 and 1
        graph.AdjacencyMatrix[1,0] = 1;
        graph.AdjacencyMatrix[1,2] = 1; // Edge between 1 and 2
        graph.AdjacencyMatrix[2,1] = 1;

        // Act
        int[] components = GraphAlgorithms.ConnectivityComponents(graph);

        // Assert
        Assert.That(components[0], Is.EqualTo(1)); // All nodes should be in the same component
        Assert.That(components[1], Is.EqualTo(1));
        Assert.That(components[2], Is.EqualTo(1));
    }

    [Test]
    public static  void TestConnectivityComponents_TwoComponents()
    {
        // Arrange
        var graph = new Graph(4); // Graph with 4 nodes
        graph.AdjacencyMatrix[0,1] = 1; // Component 1: 0-1
        graph.AdjacencyMatrix[1,0] = 1;
        graph.AdjacencyMatrix[2,3] = 1; // Component 2: 2-3
        graph.AdjacencyMatrix[3,2] = 1;

        // Act
        int[] components = GraphAlgorithms.ConnectivityComponents(graph);

        // Assert
        Assert.That(components[0], Is.EqualTo(1)); // Node 0 in component 1
        Assert.That(components[1], Is.EqualTo(1)); // Node 1 in component 1
        Assert.That(components[2], Is.EqualTo(2)); // Node 2 in component 2
        Assert.That(components[3], Is.EqualTo(2)); // Node 3 in component 2
    }

    [Test]
    public  static  void TestConnectivityComponents_DisconnectedNodes()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 isolated nodes

        // Act
        int[] components = GraphAlgorithms.ConnectivityComponents(graph);

        // Assert
        Assert.That(components[0], Is.EqualTo(1)); // Each node is its own component
        Assert.That(components[1], Is.EqualTo(2));
        Assert.That(components[2], Is.EqualTo(3));
    }
}