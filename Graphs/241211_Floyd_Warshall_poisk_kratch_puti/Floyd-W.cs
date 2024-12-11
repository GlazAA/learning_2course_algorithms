using System;

class FloydWarshall
{
    const double INF = double.PositiveInfinity;

    static void Main()
    {
        int n = 4; 
        double[,,] A = new double[n + 1, n + 1, n + 1];

        double[,] graph = {
            { 0, 3, INF, INF },
            { INF, 0, 1, INF },
            { INF, INF, 0, 2 },
            { 1, INF, INF, 0 }
        };
		
		// первое for для базовых сл
        for (int v = 1; v <= n; v++)
        {
            for (int w = 1; w <= n; w++)
            {
                if (v == w)
                {
                    A[0, v, w] = 0;
                }
                else if (graph[v - 1, w - 1] != INF)
                {
                    A[0, v, w] = graph[v - 1, w - 1];
                }
                else
                {
                    A[0, v, w] = INF;
                }
            }
        }

        // основной цикл алгоритма 
        for (int k = 1; k <= n; k++)
        {
            for (int v = 1; v <= n; v++)
            {
                for (int w = 1; w <= n; w++)
                {
                    A[k, v, w] = Math.Min(A[k - 1, v, w], A[k - 1, v, k] + A[k - 1, k, w]);
                }
            }
        }

        // проверка на наличие отрицательных циклов
        for (int v = 1; v <= n; v++)
        {
            if (A[n, v, v] < 0)
            {
                Console.WriteLine("Граф содержит отрицательный цикл.");
                return;
            }
        }


        Console.WriteLine("Расстояния между всеми парами вершин:");
        for (int v = 1; v <= n; v++)
        {
            for (int w = 1; w <= n; w++)
            {
                if (A[n, v, w] == INF)
                {
                    Console.WriteLine($"Расстояние от {v} до {w}: бесконечность");
                }
                else
                {
                    Console.WriteLine($"Расстояние от {v} до {w}: {A[n, v, w]}");
                }
            }
        }
    }
}
