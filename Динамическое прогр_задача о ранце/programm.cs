using System;
using System.Collections.Generic;

class Program
{
    static int[,] memo; 
    static List<int>[] graph; 
    static int[] weights;
    static bool[] included; 

 
    static void Main(string[] args)
    {
        
        int n = 10; 
        weights = new int[n];
        Random rand = new Random();

       
        for (int i = 0; i < n; i++)
        {
            weights[i] = rand.Next(1, 10); // Вес от 1 до 9
        }

      
        graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = new List<int>();
        }

   
        for (int i = 1; i < n; i++)
        {
            int parent = rand.Next(0, i); 
            graph[parent].Add(i);
        }

    
        memo = new int[n, 2];
        for (int i = 0; i < n; i++)
        {
            memo[i, 0] = -1; 
            memo[i, 1] = -1; 
        }

     
        int maxWeight = GetMaxWeight(0, false);
        Console.WriteLine($"Максимальный вес независимого множества: {maxWeight}");

     
        Console.WriteLine("Включенные вершины:");
        Reconstruct(0, false);
    }


    
    // метод с кэшированием
    static int GetMaxWeight(int node, bool includedNode)
    {
        if (node >= weights.Length) return 0; 

        if (memo[node, includedNode ? 1 : 0] != -1)
            return memo[node, includedNode ? 1 : 0];

        int weightWithNode = 0;
        if (!includedNode)
        {
            weightWithNode = weights[node]; 
            foreach (var child in graph[node])
            {
                weightWithNode += GetMaxWeight(child, false); // один вариант
            }
        }

        int weightWithoutNode = 0;
        foreach (var child in graph[node])
        {
            weightWithoutNode += GetMaxWeight(child, true); // второй, без включения
        }

      
        memo[node, includedNode ? 1 : 0] = Math.Max(weightWithNode, weightWithoutNode);
        return memo[node, includedNode ? 1 : 0];
    }


    static void Reconstruct(int node, bool includedNode)
    {
        if (node >= weights.Length) return;

        if (!includedNode)
        {
            Console.WriteLine($"Вершина {node} (вес: {weights[node]}) включена.");
            foreach (var child in graph[node])
            {
                Reconstruct(child, false); 
            }
        }
        else
        {
            foreach (var child in graph[node])
            {
                Reconstruct(child, true); 
            }
        }
    }
}
