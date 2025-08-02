namespace EvoGraph.Graph;

public class GraphAlgorithms
{
    /// <summary> Search for connectivity components. </summary>
    /// <returns> An array where the [index] corresponds to the node number
    /// and the [value] corresponds to the connectivity component number. </returns>
    public static int[] ConnectivityComponents(Graph graph)
    {
        var num = 1;
        var components = new int[graph.Count];
        for (var i = 0; i < graph.Count; i++)
            if (components[i] == 0)
            {
                NumDfs(i);
                num++;
            }
        return components;
        
        void NumDfs(int curr) // Numbered DFS
        {
            components[curr] = num;
            for (var next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] >= 0 || graph.AdjacencyMatrix[next, curr] >= 0) 
                    if (components[next] == 0) NumDfs(next);
        }
    }
    
    /// <summary> Depth-First Search for cases when we need all nodes
    /// no matter from which node we've started. </summary>
    /// <returns> List of vertices in the order of traversal. </returns>
    public static List<int> DfsOrderIndependent(Graph graph, int node)
    {
        if (node < 0 || node >= graph.Count) throw new ArgumentException("Invalid start node");
        
        var used = new bool[graph.Count];
        var order = new List<int>();

        Dfs(node);
        return order;
        
        void Dfs(int curr)
        {
            order.Add(curr);
            used[curr] = true;
            for (var next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] >= 0 ||
                    graph.AdjacencyMatrix[next, curr] >= 0) 
                    if (!used[next]) Dfs(next);
        }
    }
    
    /// <summary> Depth-First Search for cases when the graph edges directions are important. </summary>
    /// <returns> List of vertices in the order of traversal. </returns>
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
            for (var next = 0; next < graph.Count; next++)
                if (!used[next] && graph.AdjacencyMatrix[curr, next] >= 0) 
                    Dfs(next);
        }
    }
    
    /// <summary> Breadth-First Search for cases when we need all nodes
    /// no matter from which node we've started. </summary>
    /// <returns> List of vertices in the order of traversal. </returns>
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
            var curr = queue.Dequeue();
            order.Add(curr);

            for (var next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] > -1 || graph.AdjacencyMatrix[curr, next] > -1)
                {
                    if (used[next]) continue;
                    used[next] = true;
                    queue.Enqueue(next);
                }
        }
        return order;
    }
    
    /// <summary> Breadth-First Search for cases when the graph edges directions are important. </summary>
    /// <returns> List of vertices in the order of traversal. </returns>
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
            var curr = queue.Dequeue();
            order.Add(curr);

            for (var next = 0; next < graph.Count; next++)
                if (graph.AdjacencyMatrix[curr, next] > -1 && !used[next])
                {
                    used[next] = true;
                    queue.Enqueue(next);
                }
        }
        return order;
    }

    /// <summary> Travelling Salesman Problem aka (fr.) Commis Voyageur. </summary>
    /// <returns> Array of vertices in the order of traversal. </returns>
    public static int[] Tsp(Graph graph)
    {
        List<int> array = [];
        for (var i = 0; i < graph.Count; i++) array.Add(i);
        
        var min = double.MaxValue;
        var minList = new int[graph.Count];
        GetNextPermutation(array, graph.Count);
        return minList;
        
        void GetNextPermutation(List<int> order, int n)
        {
            if (n == 1)
            {
                // Check for a gamilton cycle
                double sum = 0;
                for (var i = 1; i < order.Count; i++)
                {
                    var w = graph.AdjacencyMatrix[order[i - 1], order[i]];
                    if (w < 0) return;
                    sum += w;
                }

                // Remember the cost
                if (sum < min)
                {
                    min = sum;
                    order.CopyTo(minList);
                }
                
            }

            else
                for (var i = 0; i < n; i++)
                {
                    GetNextPermutation(order, n - 1);
                    if (n % 2 == 0)
                        (order[i], order[n - 1]) = (order[n - 1], order[i]);
                    else
                        (order[0], order[n - 1]) = (order[n - 1], order[0]);
                }
        }
    }
    
    /// <summary> Counts the path cost, where the path is an array,
    /// [index] is the current node, [value] is the next node. </summary>
    public static double PathCost(Graph graph, int[] path, int node)
    {
        if (node < 0 || node >= graph.Count) throw new ArgumentException("Invalid start node");

        var cost = 0.0;
        for (var i = 1; i < path.Length; i++)
        {
            var next = path[i];
            if (graph.AdjacencyMatrix[node, next] < 0) // check if the path contains unexisted edges
                throw new ArgumentException("Invalid node path");
            cost += graph.AdjacencyMatrix[node, next];
            node = next;
        }

        return cost;
    }
}
