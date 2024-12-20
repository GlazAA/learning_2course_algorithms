using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    const int V = 5; 

  
    static int MinKeyWithoutHeap(int[] key, bool[] mstSet)
    {
        int min = int.MaxValue, minIndex = -1;

        for (int v = 0; v < V; v++)
            if (!mstSet[v] && key[v] < min)
            {
                min = key[v];
                minIndex = v;
            }

        return minIndex;
    }


    static void PrintMST(int[] parent, int[,] graph)
    {
        Console.WriteLine("Edge \tWeight");
        for (int i = 1; i < V; i++)
            Console.WriteLine($"{parent[i]} - {i} \t{graph[parent[i], i]}");
    }

 
    static void PrimMSTWithoutHeap(int[,] graph)
    {
        int[] parent = new int[V];
        int[] key = new int[V];
        bool[] mstSet = new bool[V];

        for (int i = 0; i < V; i++)
        {
            key[i] = int.MaxValue;
            mstSet[i] = false;
        }

        key[0] = 0;
        parent[0] = -1;

        for (int count = 0; count < V - 1; count++)
        {
            int u = MinKeyWithoutHeap(key, mstSet);
            mstSet[u] = true;

            for (int v = 0; v < V; v++)
                if (graph[u, v] != 0 && !mstSet[v] && graph[u, v] < key[v])
                {
                    parent[v] = u;
                    key[v] = graph[u, v];
                }
        }

        PrintMST(parent, graph);
    }



    // с использованием кучи
    static void PrimMSTWithHeap(int[,] graph)
    {
     
        SortedSet<(int key, int vertex)> minHeap = new SortedSet<(int, int)>();
        int[] parent = new int[V];
        int[] key = new int[V];
        bool[] mstSet = new bool[V];

        for (int i = 0; i < V; i++)
        {
            key[i] = int.MaxValue;
            mstSet[i] = false;
        }

        key[0] = 0;
        parent[0] = -1;
        minHeap.Add((0, 0)); 

        while (minHeap.Count > 0)
        {
            var (minKey, u) = minHeap.Min;
            minHeap.Remove(minHeap.Min);
            mstSet[u] = true;

            for (int v = 0; v < V; v++)
            {
                if (graph[u, v] != 0 && !mstSet[v] && graph[u, v] < key[v])
                {
                    parent[v] = u;
                    key[v] = graph[u, v];
                    minHeap.Add((key[v], v)); 
                }
            }
        }

        PrintMST(parent, graph);
    }

    static void Main(string[] args)
    {
        int[,] graphSmall = {
            { 0, 2, 0, 6, 0 },
            { 2, 0, 3, 8, 5 },
            { 0, 3, 0, 0, 7 },
            { 6, 8, 0, 0, 9 },
            { 0, 5, 7, 9, 0 }
        };

        int[,] graphMedium = {
            { 0, 2, 0, 6, 0, 0, 0, 0 },
            { 2, 0, 3, 8, 5, 0, 0, 0 },
            { 0, 3, 0, 0, 7, 0, 0, 0 },
            { 6, 8, 0, 0, 9, 0, 0, 0 },
            { 0, 5, 7, 9, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 4 },
            { 0, 0, 0, 0, 0, 1, 0, 2 },
            { 0, 0, 0, 0, 0, 4, 2, 0 }
        };

        int[,] graphLarge = new int[100, 100]; 
        Random rand = new Random();
        for (int i = 0; i < 100; i++)
            for (int j = 0; j < 100; j++)
                graphLarge[i, j] = (i != j) ? rand.Next(1, 10) : 0;

       


	   // Тестирование с маленьким графом
        Console.WriteLine("Testing small graph:");
        Stopwatch sw = new Stopwatch();
        
        sw.Start();
        PrimMSTWithoutHeap(graphSmall);
        sw.Stop();
        Console.WriteLine($"Time without heap: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        PrimMSTWithHeap(graphSmall);
        sw.Stop();
        Console.WriteLine($"Time with heap: {sw.ElapsedMilliseconds} ms");

        // Тестирование со средним графом
        Console.WriteLine("\nTesting medium graph:");
        
        sw.Restart();
        PrimMSTWithoutHeap(graphMedium);
        sw.Stop();
        Console.WriteLine($"Time without heap: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        PrimMSTWithHeap(graphMedium);
        sw.Stop();
        Console.WriteLine($"Time with heap: {sw.ElapsedMilliseconds} ms");

        // Тестирование с большим графом
        Console.WriteLine("\nTesting large graph:");
        
        sw.Restart();
        PrimMSTWithoutHeap(graphLarge);
        sw.Stop();
        Console.WriteLine($"Time without heap: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        PrimMSTWithHeap(graphLarge);
        sw.Stop();
        Console.WriteLine($"Time with heap: {sw.ElapsedMilliseconds} ms");
    }
}
