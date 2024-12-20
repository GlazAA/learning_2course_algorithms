using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Random random = new Random();
        int n = 10;
        int[] values = new int[n];
        int[] sizes = new int[n];
        int capacity = 100;

        for (int i = 0; i < n; i++)
        {
            values[i] = random.Next(50, 201);
            sizes[i] = random.Next(10, 51);
        }

        var (maxValue, A) = Knapsack(values, sizes, capacity);
        List<int> selectedItems = ReconstructSolution(A, values, sizes, capacity);
        List<int> selectedValues = new List<int>();

        foreach (int index in selectedItems)
        {
            selectedValues.Add(values[index]);
        }

        Console.WriteLine($"Maximum value: {maxValue}");
        Console.WriteLine($"Selected items (indices): [{string.Join(", ", selectedItems)}]");
        Console.WriteLine($"Value of selected items: [{string.Join(", ", selectedValues)}]");
    }

    static (int, int[,]) Knapsack(int[] values, int[] sizes, int capacity)
    {
        int n = values.Length;
        int[,] A = new int[n + 1, capacity + 1];

        for (int i = 1; i <= n; i++)
        {
            for (int c = 0; c <= capacity; c++)
            {
                if (sizes[i - 1] <= c)
                {
                    A[i, c] = Math.Max(A[i - 1, c], A[i - 1, c - sizes[i - 1]] + values[i - 1]);
                }
                else
                {
                    A[i, c] = A[i - 1, c];
                }
            }
        }

        return (A[n, capacity], A);
    }

    static List<int> ReconstructSolution(int[,] A, int[] values, int[] sizes, int capacity)
    {
        int n = values.Length;
        List<int> selectedItems = new List<int>();
        int c = capacity;

        for (int i = n; i > 0; i--)
        {
            if (sizes[i - 1] <= c && A[i, c] == A[i - 1, c - sizes[i - 1]] + values[i - 1])
            {
                selectedItems.Add(i - 1);
                c -= sizes[i - 1];
            }
        }

        selectedItems.Reverse(); 
        return selectedItems;
    }
}
