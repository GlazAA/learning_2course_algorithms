using System;
using System.Collections.Generic;
using System.Linq;

public class DFSShortestPath
{
    public static List<int> FindAnyPathDFS(int startNode, int endNode, Dictionary<int, List<int>> graph)
    {
        var visited = new HashSet<int>();
        var path = new List<int>();

         bool foundPath = FindPathDFS(startNode, endNode, graph, visited, path);

        if(foundPath) {
             return path;
        } else {
            return null;
        }
    }

    private static bool FindPathDFS(int currentNode, int endNode, Dictionary<int, List<int>> graph, HashSet<int> visited, List<int> path)
    {
          visited.Add(currentNode);
          path.Add(currentNode);
            
            if (currentNode == endNode)
            {
                return true;
            }


            if (graph.ContainsKey(currentNode))
            {
                foreach (var neighbor in graph[currentNode])
                {
                    if(!visited.Contains(neighbor)) {
                        bool found = FindPathDFS(neighbor, endNode, graph, visited, path);
                        if(found) {
                            return true;
                        }
                     }
                }
            }
        path.RemoveAt(path.Count - 1); 
        return false;
    }

    public static void Main(string[] args)
    {
        
        var graph = new Dictionary<int, List<int>>
        {
            { 0, new List<int> { 1, 2 } },
            { 1, new List<int> { 3 } },
            { 2, new List<int> { 3 } },
            { 3, new List<int>() },
             { 4, new List<int> { 5 } },
              { 5, new List<int> { } }
        };

        int start = 0;
        int end = 3;
        List<int> path = FindAnyPathDFS(start, end, graph);

       if (path != null) {
             Console.WriteLine($"Путь от {start} до {end}: {string.Join(" -> ", path)}");
        } else {
            Console.WriteLine("Путь не найден");
        }

           start = 4;
           end = 3;
           path = FindAnyPathDFS(start, end, graph);

       if (path != null) {
             Console.WriteLine($"Путь от {start} до {end}: {string.Join(" -> ", path)}");
        } else {
            Console.WriteLine("Путь не найден");
        }
    }
}