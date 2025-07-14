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
        Assert.That(1, Is.EqualTo(components[0])); // All nodes should be in the same component
        Assert.That(1, Is.EqualTo(components[1]));
        Assert.That(1, Is.EqualTo(components[2]));
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
        Assert.That(1, Is.EqualTo(components[0])); // Node 0 in component 1
        Assert.That(1, Is.EqualTo(components[1])); // Node 1 in component 1
        Assert.That(2, Is.EqualTo(components[2])); // Node 2 in component 2
        Assert.That(2, Is.EqualTo(components[3])); // Node 3 in component 2
    }

    [Test]
    public  static  void TestConnectivityComponents_DisconnectedNodes()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 isolated nodes

        // Act
        int[] components = GraphAlgorithms.ConnectivityComponents(graph);

        // Assert
        Assert.That(1, Is.EqualTo(components[0])); // Each node is its own component
        Assert.That(2, Is.EqualTo(components[1]));
        Assert.That(3, Is.EqualTo(components[2]));
    }
}