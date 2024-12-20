using System;
using System.Collections.Generic;
using System.Linq;

public class Task
{
    public int Weight { get; set; }
    public int Duration { get; set; }

    public Task(int weight, int duration)
    {
        Weight = weight;
        Duration = duration;
    }
}

public class WeightedCompletionTime
{
    public static int MinimizeWeightedCompletionTime(List<Task> tasks)
    {
    
        var sortedTasks = tasks.OrderBy(t => t.Duration).ToList();

        int totalCompletionTime = 0;
        int currentTime = 0;

        foreach (var task in sortedTasks)
        {
            currentTime += task.Duration; 
            totalCompletionTime += currentTime * task.Weight; 
        }

        return totalCompletionTime;
    }

    public static void Test(int numberOfTasks)
    {
        Random random = new Random();
        var tasks = new List<Task>();

        
        for (int i = 0; i < numberOfTasks; i++)
        {
            int weight = random.Next(1, 101); 
            int duration = random.Next(1, 101);
            tasks.Add(new Task(weight, duration));
        }

        int result = MinimizeWeightedCompletionTime(tasks);
        Console.WriteLine($"Минимальная сумма взвешенных сроков завершения для {numberOfTasks} задач: {result}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        int numberOfTasks = 100000; 
        WeightedCompletionTime.Test(numberOfTasks);
    }
}
using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int U, V, Weight;

    public int CompareTo(Edge other)
    {
        return Weight.CompareTo(other.Weight);
    }
}

class DisjointSet
{
    private Dictionary<int, int> parent = new Dictionary<int, int>();
    private Dictionary<int, int> rank = new Dictionary<int, int>();

    public void MakeSet(int v)
    {
        parent[v] = v;
        rank[v] = 0;
    }

    public int FindSet(int v)
    {
        if (parent[v] != v)
        {
            parent[v] = FindSet(parent[v]); 
        }
        return parent[v];
    }

    public void Union(int u, int v)
    {
        int rootU = FindSet(u);
        int rootV = FindSet(v);

        if (rootU != rootV)
        {
            if (rank[rootU] < rank[rootV])
            {
                parent[rootU] = rootV;
            }
            else if (rank[rootU] > rank[rootV])
            {
                parent[rootV] = rootU;
            }
            else
            {
                parent[rootV] = rootU;
                rank[rootU]++;
            }
        }
    }
}

class KruskalWithDisjointSet
{
    public List<Edge> Execute(List<Edge> edges, int vertexCount)
    {
        edges.Sort();
        DisjointSet ds = new DisjointSet();
        for (int i = 0; i < vertexCount; i++)
        {
            ds.MakeSet(i);
        }

        List<Edge> mst = new List<Edge>();
        foreach (var edge in edges)
        {
            if (ds.FindSet(edge.U) != ds.FindSet(edge.V))
            {
                mst.Add(edge);
                ds.Union(edge.U, edge.V);
            }
        }
        return mst;
    }
}

class KruskalWithoutDisjointSet
{
    private List<int>[] adjacencyList;

    public KruskalWithoutDisjointSet(int vertexCount)
    {
        adjacencyList = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            adjacencyList[i] = new List<int>();
        }
    }

    private bool AreConnected(int u, int v)
    {
      
        return adjacencyList[u].Contains(v) || adjacencyList[v].Contains(u);
    }

    private void Connect(int u, int v)
    {
        adjacencyList[u].Add(v);
        adjacencyList[v].Add(u);
    }

    public List<Edge> Execute(List<Edge> edges, int vertexCount)
    {
        edges.Sort();
        List<Edge> mst = new List<Edge>();

        foreach (var edge in edges)
        {
            if (!AreConnected(edge.U, edge.V))
            {
                mst.Add(edge);
                Connect(edge.U, edge.V);
            }
        }
        return mst;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int vertexCount = 1000;
        List<Edge> edges = GenerateRandomEdges(vertexCount, 5000); 

        // Сравнение с использованием структуры пересекающихся множеств
        var kruskalWithDS = new KruskalWithDisjointSet();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var mstWithDS = kruskalWithDS.Execute(edges, vertexCount);
        watch.Stop();
        Console.WriteLine($"Краскал с Disjoint Set: {watch.ElapsedMilliseconds} мс");

        // Сравнение без использования структуры пересекающихся множеств
        var kruskalWithoutDS = new KruskalWithoutDisjointSet(vertexCount);
        watch = System.Diagnostics.Stopwatch.StartNew();
        var mstWithoutDS = kruskalWithoutDS.Execute(edges, vertexCount);
        watch.Stop();
        Console.WriteLine($"Краскал без Disjoint Set: {watch.ElapsedMilliseconds} мс");
    }

    static List<Edge> GenerateRandomEdges(int vertexCount, int edgeCount)
    {
        Random random = new Random();
        HashSet<Edge> edgeSet = new HashSet<Edge>();
        while (edgeSet.Count < edgeCount)
        {
            int u = random.Next(vertexCount);
            int v = random.Next(vertexCount);
            if (u != v)
            {
                int weight = random.Next(1, 100);
                edgeSet.Add(new Edge { U = u, V = v, Weight = weight });
            }
        }
        return new List<Edge>(edgeSet);
    }
}
