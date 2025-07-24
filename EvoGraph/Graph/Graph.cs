using System.Numerics;
using EvoGraph.MathUtils;

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
        AdjacencyMatrix = ArrayUtils.MatrixWithValue(size, -1);
        for (int i = 0; i < size; i++)
            AdjacencyMatrix[i, i] = 0;
    }

    /// <summary>
    /// Add a new node to the graph.
    /// </summary>
    public void AddNode()
    {
        double[,] matrix = new double[Count + 1, Count + 1];
        for (int row = 0; row < Count; row++)
        {
            for (int col = 0; col < Count; col++)
                matrix[row, col] = AdjacencyMatrix[row, col];
            matrix[row, Count] = -1;
        }
        
        for (int col = 0; col < Count; col++)
            matrix[Count, col] = -1;
        matrix[Count, Count] = 0;
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
        for (int row = 0; row < Count; row++)
        { 
            for (int col = 0; col < Count; col++)
                str += AdjacencyMatrix[row, col] + " ";
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
        for (int row = 1; row <= count; row++)
        {
            string[] values = lines[row].Trim().Split(' ');
            if (values.Length != count) 
                throw new Exception("Incorrect number of values");
            for (int col = 0; col < count; col++)
                graph.AdjacencyMatrix[row - 1, col] = double.Parse(values[col]);
        }

        return graph;
    }

    /// <summary>
    /// Copies the graph adjacency matrix.
    /// </summary>
    /// <returns>
    /// The new instance of 2D array with same values as
    /// the graph adjacency matrix.
    /// </returns>
    public double[,] CopyAdjacencyMatrix()
    {
        double[,] matrix = new double[Count, Count];
        for (int row = 0;  row < Count;  row++)
        for (int col = 0; col < Count; col++)
            matrix[row, col] = AdjacencyMatrix[ row, col]; 
        return matrix;
    }
}
