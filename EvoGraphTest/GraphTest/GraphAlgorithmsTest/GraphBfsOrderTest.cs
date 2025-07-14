using EvoGraph.Graph;

namespace EvoGraphTest.GraphTest.GraphAlgorithmsTest;

public class GraphBfsOrderTests
{
    [Test]
    public void TestBfsOrderIndependent_ConnectedGraph()
    {
        // Arrange
        var graph = new Graph(4); // Graph with 4 nodes
        graph.AdjacencyMatrix[0,1] = 1; // Edges: 0-1, 1-2, 1-3
        graph.AdjacencyMatrix[1,0] = 1;
        graph.AdjacencyMatrix[1,2] = 1;
        graph.AdjacencyMatrix[2,1] = 1;
        graph.AdjacencyMatrix[1,3] = 1;
        graph.AdjacencyMatrix[3,1] = 1;

        // Act
        var order = GraphAlgorithms.BfsOrderIndependent(graph, 0); // Start from node 0

        // Assert
        Assert.That(order, Has.Member(0)); // Should include start node
        Assert.That(order, Has.Member(1));
        Assert.That(order, Has.Member(2));
        Assert.That(order, Has.Member(3));
        Assert.That(order.Count, Is.EqualTo(4)); // All nodes should be visited
        Assert.That(order.IndexOf(0) < order.IndexOf(1)); // 0 should come before 1
        Assert.That(order.IndexOf(1) < order.IndexOf(2) || order.IndexOf(1) < order.IndexOf(3)); // 1 before 2 or 3
    }

    [Test]
    public void TestBfsOrderIndependent_DisconnectedGraph()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 nodes, 0-1 connected, 2 isolated
        graph.AdjacencyMatrix[0,1] = 1;
        graph.AdjacencyMatrix[1,0] = 1;

        // Act
        var order = GraphAlgorithms.BfsOrderIndependent(graph, 0); // Start from node 0

        // Assert
        Assert.That(order, Has.Member(0)); // Should include 0 and 1
        Assert.That(order, Has.Member(1));
        Assert.That(order.Count, Is.EqualTo(2)); // Only connected nodes visited
        Assert.That(order, Has.No.Member(2)); // Isolated node 2 not visited
    }
    
    [Test]
    public void TestBfsOrderIndependent_InvalidStartNode()
    {
        // Arrange
        var graph = new Graph(2);

        // Act
        Assert.Throws<ArgumentException>(() => GraphAlgorithms.BfsOrderIndependent(graph, 2)); // Invalid start node (out of range)
    }
}
