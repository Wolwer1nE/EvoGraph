using System.Numerics;

namespace EvoGraph.Graph;


public class Graph
{
    public int Count { get; set; }
    public double[,] AdjacencyMatrix;
    
    /// <summary>
    /// Create an empty graph with <b>size</b> nodes.
    /// </summary>
    public Graph(int size)
    {
        Count = size;
        AdjacencyMatrix = new double[size, size];
        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
            AdjacencyMatrix[i, j] = -1; 
    }

    /// <summary>
    /// Add a new node to the graph.
    /// </summary>
    public void AddNode()
    {
        double[,] matrix = new double[Count + 1, Count + 1];
        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
                matrix[i, j] = AdjacencyMatrix[i, j];
            matrix[i, Count] = -1;
        }
        
        for (int i = 0; i < Count; i++)
            matrix[Count, i] = -1;
        matrix[Count, Count] = -1;
        Count = Count + 1;
        AdjacencyMatrix = matrix;
    }

    /// <summary>
    /// Add a new edge to graph or modify existed.
    /// </summary>
    /// <param name="from">
    /// <b>X</b> coordinate in adjacency matrix.
    /// </param>
    /// <param name="to">
    /// <b>Y</b> coordinate in adjacency matrix.
    /// </param>
    /// <param name="weight">
    /// The weight of an edge
    /// </param>
    public void AddEdge(int from, int to, double weight)
    {
        if (from < 0 || from >= Count || to < 0 || to >= Count) 
            throw new ArgumentOutOfRangeException();
        AdjacencyMatrix[from, to] = weight; 
    }

    /// <summary>
    /// Convert the graph to a string for serialization.
    /// </summary>
    public override string? ToString()
    {
        string str = Count + "\n";
        for (int i = 0; i < Count; i++)
        { 
            for (int j = 0; j < Count; j++)
                str += AdjacencyMatrix[i, j] + " ";
            str += "\n";
        }
        
        return str;
    }
    
    /// <summary>
    /// Create a graph from string (deserialize).
    /// </summary>
    /// <param name="lines">
    /// Array of strings representing a graph (string split
    /// by a <b>'\n'</b> symbol). 
    /// </param>
    /// <returns>
    /// The new graph.
    /// </returns>
    public static Graph FromString(string[] lines)
    {
        var count = int.Parse(lines[0]);
        var graph = new Graph(count);
        for (int i = 1; i <= count; i++)
        {
            string[] values = lines[i].Trim().Split(' ');
            if (values.Length != count) 
                throw new Exception("Incorrect number of values");
            for (int j = 0; j < count; j++)
                graph.AdjacencyMatrix[i - 1, j] = double.Parse(values[j]);
        }

        return graph;
    }
}
