using EvoGraph.MathUtils;

namespace EvoGraph.Graph;

public class Graph
{
    public int Count { get; set; }
    
    public double[,] AdjacencyMatrix;
    
    /// <summary> Create an empty graph with [size] nodes. </summary>
    public Graph(int size)
    {
        Count = size;
        AdjacencyMatrix = ArrayUtils.MatrixWithValue(size, -1.0);
        for (var i = 0; i < size; i++)
            AdjacencyMatrix[i, i] = 0;
    }
    
    public void AddNode()
    {
        var matrix = new double[Count + 1, Count + 1];
        for (var row = 0; row < Count; row++)
        {
            for (var col = 0; col < Count; col++)
                matrix[row, col] = AdjacencyMatrix[row, col];
            matrix[row, Count] = -1;
        }
        
        for (var col = 0; col < Count; col++)
            matrix[Count, col] = -1;
        matrix[Count, Count] = 0;
        Count += 1;
        AdjacencyMatrix = matrix;
    }

    /// <summary> Add a new edge to graph or modify existed. </summary>
    public void AddEdge(int from, int to, double weight)
    {
        if (from < 0 || from >= Count || to < 0 || to >= Count) 
            throw new ArgumentException("to or from are out of range");
        AdjacencyMatrix[from, to] = weight; 
    }

    /// <summary> Convert the graph to a string for serialization. </summary>
    public override string ToString()
    {
        var str = $"{Count}\n";
        for (int row = 0; row < Count; row++)
        { 
            for (int col = 0; col < Count; col++)
                str += $"{AdjacencyMatrix[row, col]} ";
            str += "\n";
        }
        
        return str;
    }
    
    /// <summary> Deserialize a graph from string. </summary>
    public static Graph FromString(string[] lines)
    {
        var count = int.Parse(lines[0]);
        var graph = new Graph(count);
        for (var row = 1; row <= count; row++)
        {
            var values = lines[row].Trim().Split(' ');
            if (values.Length != count) throw new Exception("Incorrect number of values");
            for (var col = 0; col < count; col++) graph.AdjacencyMatrix[row - 1, col] = double.Parse(values[col]);
        }

        return graph;
    }

    /// <summary> Deep clones the graph adjacency matrix. </summary>
    public double[,] CloneAdjacencyMatrix()
    {
        var matrix = new double[Count, Count];
        for (var row = 0;  row < Count;  row++)
        for (var col = 0; col < Count; col++)
            matrix[row, col] = AdjacencyMatrix[ row, col]; 
        return matrix;
    }
}
