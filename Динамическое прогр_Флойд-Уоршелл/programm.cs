using System;

class Program
{
    const double Infinity = double.PositiveInfinity;

    static double[,,] FloydWarshall(double[,] graph, int n)
    {
        double[,,] A = new double[n + 1, n, n];

    
        for (int v = 0; v < n; v++) 
        {
            for (int w = 0; w < n; w++) 
            {
                if (v == w)
                    A[0, v, w] = 0;
                else if (graph[v, w] != Infinity)
                    A[0, v, w] = graph[v, w];
                else
                    A[0, v, w] = Infinity;
            }
        }

        for (int k = 1; k <= n; k++) 
        {
            for (int v = 0; v < n; v++)
            {
                for (int w = 0; w < n; w++)
                {
                    A[k, v, w] = Math.Min(A[k - 1, v, w], A[k - 1, v, k - 1] + A[k - 1, k - 1, w]);
                }
            }
        }

        for (int v = 0; v < n; v++)
        {
            if (A[n, v, v] < 0)
                return null; 
        }

        return A[n];
    }

    static void ShowResult(double[,] result, int n)
    {
        if (result == null)
        {
            Console.WriteLine("Есть отрицательный цикл");
            return;
        }

        for (int v = 0; v < n; v++)
        {
            for (int w = 0; w < n; w++)
            {
                Console.WriteLine($"dist({v + 1}, {w + 1}) = {result[v, w]}");
            }
        }
    }

    static double[,] GenerateRandomGraph(int n, double density)
    {
        Random rand = new Random();
        double[,] graph = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    graph[i, j] = 0;
                else if (rand.NextDouble() < density)
                    graph[i, j] = rand.Next(1, 10); 
                else
                    graph[i, j] = Infinity;
            }
        }

        return graph;
    }



    static void Main(string[] args)
    {
       
        double[,] graph1 = {
            { 0, 1, Infinity, Infinity, Infinity },
            { Infinity, 0, -2, Infinity, 4 },
            { Infinity, Infinity, 0, -3, Infinity },
            { Infinity, -1, Infinity, 0, Infinity },
            { Infinity, Infinity, Infinity, Infinity, 0 }
        };

        double[,] graph2 = {
            { 0, 2, 5, Infinity },
            { Infinity, 0, 1, Infinity },
            { Infinity, Infinity, 0, -3 },
            { -4, Infinity, Infinity, 0 }
        };

        double[,] graph3 = {
            { 0, 3, 8, Infinity },
            { Infinity, 0, Infinity, 2 },
            { Infinity, Infinity, 0, 1 },
            { Infinity, Infinity, Infinity, 0 }
        };

       
        var result1 = FloydWarshall(graph1, 5);
        var result2 = FloydWarshall(graph2, 4);
        var result3 = FloydWarshall(graph3, 4);

        ShowResult(result1, 5);
        ShowResult(result2, 4);
        ShowResult(result3, 4);

     
        int largeGraphSize = 100; // Размер большого графа
        double density = 0.1; // Плотность графа 

        double[,] largeGraph = GenerateRandomGraph(largeGraphSize, density);
        var largeResult = FloydWarshall(largeGraph, largeGraphSize);
        ShowResult(largeResult, largeGraphSize);
    }
}
