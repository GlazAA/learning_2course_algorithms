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
        var priorityQueue = new SortedSet<(int distance, string vertex)>(Comparer<(int, string)>.Create((x, y) => 
        {
            int result = x.distance.CompareTo(y.distance);
            return result == 0 ? x.vertex.CompareTo(y.vertex) : result;
        }));

       
        foreach (var vertex in graph.Keys)
        {
            distances[vertex] = int.MaxValue; 
        }
        distances[start] = 0; 
        priorityQueue.Add((0, start));

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentVertex) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min); 

          
            if (distances[currentVertex] < currentDistance)
            {
                continue;
            }

        
            foreach (var (neighbor, weight) in graph[currentVertex])
            {
                int newDistance = currentDistance + weight; 

               
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    priorityQueue.Add((newDistance, neighbor)); 
                }
            }
        }

        return distances; 
}
