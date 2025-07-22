using EvoGraph.Graph;

namespace EvoGraphTest.GraphTest.GraphAlgorithmsTest;

public class GraphTspTests
{
    [Test]
    public void TestTsp_ThreeVertices()
    {
        // Arrange
        var graph = new Graph(3); // Graph with 3 nodes
        graph.AdjacencyMatrix[0, 1] = 4; // Edges with weights
        graph.AdjacencyMatrix[1, 0] = 4;
        graph.AdjacencyMatrix[1, 2] = 3;
        graph.AdjacencyMatrix[2, 1] = 3;
        graph.AdjacencyMatrix[0, 2] = 5;
        graph.AdjacencyMatrix[2, 0] = 5;

        // Act
        int[] minList = GraphAlgorithms.Tsp(graph);

        // Assert
        Assert.That(minList, Has.Member(0)); // Should include all nodes
        Assert.That(minList, Has.Member(1));
        Assert.That(minList, Has.Member(2));
        Assert.That(minList.Length, Is.EqualTo(3)); // All nodes should be in the result
        // Verify a valid Hamiltonian cycle (e.g., 0->1->2->0 with total weight 12)
        double totalWeight = graph.AdjacencyMatrix[minList[0], minList[1]] +
                           graph.AdjacencyMatrix[minList[1], minList[2]] +
                           graph.AdjacencyMatrix[minList[2], minList[0]];
        Assert.That(totalWeight, Is.EqualTo(12)); // Ensure a valid path exists
    }

    [Test]
    public void TestTsp_FiveVertices()
    {
        // Arrange
        var graph = new Graph(5); // Graph with 5 nodes
        graph.AdjacencyMatrix[0, 1] = 2; // Edges with weights
        graph.AdjacencyMatrix[1, 0] = 2;
        graph.AdjacencyMatrix[1, 2] = 4;
        graph.AdjacencyMatrix[2, 1] = 4;
        graph.AdjacencyMatrix[2, 3] = 1;
        graph.AdjacencyMatrix[3, 2] = 1;
        graph.AdjacencyMatrix[3, 4] = 5;
        graph.AdjacencyMatrix[4, 3] = 5;
        graph.AdjacencyMatrix[0, 4] = 3;
        graph.AdjacencyMatrix[4, 0] = 3;

        // Act
        int[] minList = GraphAlgorithms.Tsp(graph);

        // Assert
        Assert.That(minList, Has.Member(0)); // Should include all nodes
        Assert.That(minList, Has.Member(1));
        Assert.That(minList, Has.Member(2));
        Assert.That(minList, Has.Member(3));
        Assert.That(minList, Has.Member(4));
        Assert.That(minList.Length, Is.EqualTo(5)); // All nodes should be in the result
        // Verify a valid Hamiltonian cycle (e.g., 0->1->2->3->4->0 with total weight 15)
        double totalWeight = graph.AdjacencyMatrix[minList[0], minList[1]] +
                           graph.AdjacencyMatrix[minList[1], minList[2]] +
                           graph.AdjacencyMatrix[minList[2], minList[3]] +
                           graph.AdjacencyMatrix[minList[3], minList[4]] +
                           graph.AdjacencyMatrix[minList[4], minList[0]];
        Assert.That(totalWeight, Is.EqualTo(15)); // Ensure a valid path exists
    }
}
