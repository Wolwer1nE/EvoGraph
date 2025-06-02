using EvoGraph.MathUtils;

namespace EvoGraph.Graph;

public class GraphAlgorithms
{
    /// <summary>
    /// Search for connectivity components.
    /// </summary>
    /// <returns>
    /// An array where the <b>index</b> corresponds to the node number
    /// and the <b>value</b> corresponds to the connectivity component number.
    /// </returns>
    public static int[] ConnectivityComponents(Graph graph)
    {
        int num = 1;
        var components = new int[graph.Count];
        for (int i = 0; i < graph.Count; i++)
            if (components[i] == 0)
            {
                Ndfs(i);
                num++;
            }
        return components;
        
        void Ndfs(int curr) // Numbered DFS
        {
            components[curr] = num;
            for (int next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] >= 0 || graph.AdjacencyMatrix[next, curr] >= 0) 
                    if (components[next] == 0) Ndfs(next);
        }
    }
    
    /// <summary>
    /// <b>Depth-First Search</b> for cases when we need all nodes
    /// no matter from which node we've started.
    /// </summary>
    /// <param name="node">
    /// The start node
    /// </param>
    /// <returns>
    /// List of vertices in the order of traversal.
    /// </returns>
    public static List<int> DfsOrderIndependent(Graph graph, int node)
    {
        if (node < 0 || node >= graph.Count)
            throw new ArgumentException("Invalid start node");
        
        var used = new bool[graph.Count];
        var order = new List<int>();

        Dfs(node);
        return order;
        
        void Dfs(int curr)
        {
            order.Add(curr);
            used[curr] = true;
            for (int next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] >= 0 ||
                    graph.AdjacencyMatrix[next, curr] >= 0) 
                    if (!used[next]) Dfs(next);
        }
    }
    
    /// <summary>
    /// <b>Depth-First Search</b> for cases when the graph edges directions are important.
    /// </summary>
    /// <param name="node">
    /// The start node
    /// </param>
    /// <returns>
    /// List of vertices in the order of traversal.
    /// </returns>
    public static List<int> DfsOriented(Graph graph, int node)
    {
        if (node < 0 || node >= graph.Count)
            throw new ArgumentException("Invalid start node");
        
        var used = new bool[graph.Count];
        var order = new List<int>();

        Dfs(node);
        return order;

        void Dfs(int curr)
        {
            order.Add(curr);
            used[curr] = true;
            for (int next = 0; next < graph.Count; next++)
                if (!used[next] && graph.AdjacencyMatrix[curr, next] >= 0) 
                    Dfs(next);
        }
    }
    
    /// <summary>
    /// <b>Breadth-First Search</b> for cases when we need all nodes
    /// no matter from which node we've started.
    /// </summary>
    /// <param name="node">
    /// The start node
    /// </param>
    /// <returns>
    /// List of vertices in the order of traversal.
    /// </returns>
    public static List<int> BfsOrderIndependent(Graph graph, int node)
    {
        if (node < 0 || node >= graph.Count)
            throw new ArgumentException("Invalid start node");

        var order = new List<int>();
        var used = new bool[graph.Count];
        var queue = new Queue<int>();

        used[node] = true;
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();
            order.Add(curr);

            for (int next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] > -1 || graph.AdjacencyMatrix[curr, next] > -1)
                {
                    if (used[next]) continue;
                    used[next] = true;
                    queue.Enqueue(next);
                }
        }
        return order;
    }
    
    /// <summary>
    /// <b>Breadth-First Search</b> for cases when the graph edges directions are important.
    /// </summary>
    /// <param name="node">
    /// The start node
    /// </param>
    /// <returns>
    /// List of vertices in the order of traversal.
    /// </returns>
    public static List<int> BfsOriented(in Graph graph, int node)
    {
        if (node < 0 || node >= graph.Count)
            throw new ArgumentException("Invalid start node");

        var order = new List<int>();
        var used = new bool[graph.Count];
        var queue = new Queue<int>();

        used[node] = true;
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();
            order.Add(curr);

            for (int next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] > -1 && !used[next])
                {
                    used[next] = true;
                    queue.Enqueue(next);
                }
        }
        return order;
    }

    /// <summary>
    /// Travelling Salesman Problem aka <i><b>(fr.) Commis Voyageur.</b></i>
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public static List<int> Tsp(Graph graph)
    {
        //TODO: implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Calculate the cost of the given path.
    /// </summary>
    /// <param name="path">
    /// An array where the <b>index</b> is the current node number,
    /// and the <b>value</b> is the number of the next node in the path.
    /// </param>
    /// <param name="node">
    /// The start node number;
    /// </param>
    /// <returns>
    /// The cost of the given path.
    /// </returns>
    public static double PathCost(Graph graph, int[] path, int node)
    {
        if (node < 0 || node >= graph.Count)
            throw new ArgumentException("Invalid start node");

        var cost = 0.0;
        for (int i = 0; i < path.Length; i++)
        {
            int next = path[node];
            if (graph.AdjacencyMatrix[node, next] < 0) // check if the path contains unexisted edges
                throw new ArgumentException("Invalid node path");
            cost += graph.AdjacencyMatrix[node, next];
            node = next;
        }

        return cost;
    }
}
