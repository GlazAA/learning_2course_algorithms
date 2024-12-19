using System;
using System.Collections.Generic;
using System.Linq;

public class BreadthFirstSearch
{
    public static List<T> BreadthFirstPrint<T>(Dictionary<T, List<T>> graph, T source)
    {
        var queue = new Queue<T>();
        queue.Enqueue(source);
        var result = new List<T>();

        while (queue.Count > 0)
        {
            T current = queue.Dequeue();
            result.Add(current);
            if (graph.ContainsKey(current))
            {
                 foreach (var neighbor in graph[current])
                  {
                     queue.Enqueue(neighbor);
                  }
            }
        }
        return result;
    }


    public static void RunTests()
    {
      
        var graph1 = new Dictionary<string, List<string>>
        {
            { "a", new List<string> { "b", "c" } },
            { "b", new List<string> { "d", "e" } },
            { "c", new List<string>() },
            { "d", new List<string>() },
            { "e", new List<string>() }
        };

        var expected1 = new List<string> { "a", "b", "c", "d", "e" };
        var result1 = BreadthFirstPrint(graph1, "a");
        Console.WriteLine("Test 1 - Expected: [" + string.Join(", ", expected1) + "]");
        Console.WriteLine("Test 1 - Result: [" + string.Join(", ", result1) + "]");
        if (!expected1.SequenceEqual(result1))
        {
             throw new Exception("Test 1 Failed!");
        }
         Console.WriteLine("Test 1 Passed!\n");


        // тест второй
         var graph2 = new Dictionary<string, List<string>>
        {
            {"a", new List<string> { "b", "c", "d" }},
            {"b", new List<string> { "e", "f" }},
            {"c", new List<string> { "g" }},
            {"d", new List<string> { "h", "i" }},
            {"e", new List<string>()},
            {"f", new List<string> { "j", "k" }},
            {"g", new List<string> { "l" }},
            {"h", new List<string>()},
            {"i", new List<string> { "m" }},
            {"j", new List<string>()},
            {"k", new List<string>()},
             {"l", new List<string>()},
            {"m", new List<string>()}
        };
        var expected2 = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m" };
        var result2 = BreadthFirstPrint(graph2, "a");
        Console.WriteLine("Test 2 - Expected: [" + string.Join(", ", expected2) + "]");
         Console.WriteLine("Test 2 - Result: [" + string.Join(", ", result2) + "]");
          if (!expected2.SequenceEqual(result2))
        {
            throw new Exception("Test 2 Failed!");
         }
         Console.WriteLine("Test 2 Passed!\n");
    }

    public static void Main(string[] args)
    {
        RunTests();
    }
}