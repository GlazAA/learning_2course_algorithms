using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
        var graph = new Dictionary<string, List<(string neighbor, int weight)>>
        {
            { "A", new List<(string, int)> { ("B", 1), ("C", 4) } },
            { "B", new List<(string, int)> { ("C", 2), ("D", 6) } },
            { "C", new List<(string, int)> { ("D", 3) } },
            { "D", new List<(string, int)>() }
        };

        string startVertex = "A";
        var shortestPaths = Dijkstra(graph, startVertex);

        Console.WriteLine("Shortest distance from the vertex " + startVertex);
        foreach (var kvp in shortestPaths)
        {
            Console.WriteLine($"To vertex {kvp.Key}: {kvp.Value}");
        }
    }

    static Dictionary<string, int> Dijkstra(Dictionary<string, List<(string neighbor, int weight)>> graph, string start)
    {
        var distances = new Dictionary<string, int>(); 
        var priorityQueue = new PriorityQueue<string, int>(); // Куча 

   
        foreach (var vertex in graph.Keys)
        {
            distances[vertex] = int.MaxValue; 
        }
        distances[start] = 0; 
        priorityQueue.Enqueue(start, 0); 

        while (priorityQueue.Count > 0)
        {
            var currentVertex = priorityQueue.Dequeue(); 

            foreach (var (neighbor, weight) in graph[currentVertex])
            {
                int newDistance = distances[currentVertex] + weight;

         
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    priorityQueue.Enqueue(neighbor, newDistance); 
                }
            }
        }

        return distances; 
    }
}
