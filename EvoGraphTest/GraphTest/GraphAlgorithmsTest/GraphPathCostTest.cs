using EvoGraph.Graph;

namespace EvoGraphTest.GraphTest.GraphAlgorithmsTest;

public class GraphPathCostTests
{
    [Test]
    public void TestPathCost_ValidPathThreeNodes()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 nodes
        graph.AdjacencyMatrix[0, 1] = 4; // Edges with weights
        graph.AdjacencyMatrix[1, 0] = 4;
        graph.AdjacencyMatrix[1, 2] = 3;
        graph.AdjacencyMatrix[2, 1] = 3;
        int[] path = { 0, 1, 2 }; // Path: 0 -> 1 -> 2

        // Act
        double cost = GraphAlgorithms.PathCost(graph, path, 0); // Start from node 0

        // Assert
        Assert.That(cost, Is.EqualTo(7.0)); // Expected cost: 4 (0-1) + 3 (1-2)
    }

    [Test]
    public void TestPathCost_InvalidStartNode()
    {
        // Arrange
        var graph = new Graph(3);
        graph.AdjacencyMatrix[0, 1] = 4;
        graph.AdjacencyMatrix[1, 0] = 4;
        int[] path = { 0, 1, 2 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GraphAlgorithms.PathCost(graph, path, 3)); // Invalid start node
    }

    [Test]
    public void TestPathCost_InvalidEdge()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 nodes
        graph.AdjacencyMatrix[0, 1] = 4; // Only 0-1 edge exists
        graph.AdjacencyMatrix[1, 0] = 4;
        int[] path = { 0, 1, 2 }; // Path includes non-existent edge 1-2

        // Act & Assert
        Assert.Throws<ArgumentException>(() => GraphAlgorithms.PathCost(graph, path, 0)); // Should throw due to invalid edge
    }
}
