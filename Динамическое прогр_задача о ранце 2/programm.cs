using System;
using System.Collections.Generic;

class Item
{
    public int Value { get; }
    public int Weight { get; }
    
    public Item(int value, int weight)
    {
        Value = value;
        Weight = weight;
    }
}

class Knapsack
{
    public static (int, List<Item>) Solve(int capacity, List<Item> items)
    {
        int n = items.Count;
        int[,] dp = new int[n + 1, capacity + 1];

      
        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                if (items[i - 1].Weight <= w)
                {
                    dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - items[i - 1].Weight] + items[i - 1].Value);
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

    
        List<Item> selectedItems = new List<Item>();
        int totalValue = dp[n, capacity];
        int remainingWeight = capacity;

        for (int i = n; i > 0 && totalValue > 0; i--)
        {
            if (totalValue != dp[i - 1, remainingWeight])
            {
                selectedItems.Add(items[i - 1]);
                totalValue -= items[i - 1].Value;
                remainingWeight -= items[i - 1].Weight;
            }
        }

        return (dp[n, capacity], selectedItems);
    }
}

class Program
{
    static void Main(string[] args)
    {
        TestKnapsack();
    }

    static void TestKnapsack()
    {
     
        Random rand = new Random();
        int itemCount = 100; // Количество предметов
        int maxWeight = 50; // Максимальный вес рюкзака
        List<Item> items = new List<Item>();

        for (int i = 0; i < itemCount; i++)
        {
            int weight = rand.Next(1, 10); // Случайный вес от 1 до 10
            int value = rand.Next(1, 100); // Случайная стоимость от 1 до 100
            items.Add(new Item(value, weight));
        }

        int capacity = rand.Next(100, 200); // Случайная емкость рюкзака от 100 до 200

   
        var (maxValue, selectedItems) = Knapsack.Solve(capacity, items);

    
        Console.WriteLine($"Максимальная стоимость: {maxValue}");
        Console.WriteLine($"Емкость рюкзака: {capacity}");
        Console.WriteLine("Выбранные предметы:");
        foreach (var item in selectedItems)
        {
            Console.WriteLine($"Стоимость: {item.Value}, Вес: {item.Weight}");
        }
    }
}
