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
