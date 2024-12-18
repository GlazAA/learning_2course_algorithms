using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        RunTests();
    }

    static int[] Quicksort(int[] arr)
    {
        if (arr.Length <= 1)
        {
            return arr;
        }

        int pivot = arr[0];
        int[] left = Array.FindAll(arr[1..], x => x <= pivot);
        int[] right = Array.FindAll(arr[1..], x => x > pivot);

        return Concatenate(Quicksort(left), new int[] { pivot }, Quicksort(right));
    }

    static int[] Concatenate(int[] left, int[] pivot, int[] right)
    {
        int[] result = new int[left.Length + pivot.Length + right.Length];
        Buffer.BlockCopy(left, 0, result, 0, left.Length * sizeof(int));
        Buffer.BlockCopy(pivot, 0, result, left.Length * sizeof(int), pivot.Length * sizeof(int));
        Buffer.BlockCopy(right, 0, result, (left.Length + pivot.Length) * sizeof(int), right.Length * sizeof(int));
        return result;
    }

    static int[] GenerateTestData(int size)
    {
        Random random = new Random();
        int[] data = new int[size];
        for (int i = 0; i < size; i++)
        {
            data[i] = random.Next(-1000000, 1000000);
        }
        return data;
    }

    static void RunTests()
    {
        int size = 1000000;
        int[] data = GenerateTestData(size);

        int[] dataTemp = (int[])data.Clone();

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        int[] sortedArrQuick = Quicksort(data);
        stopwatch.Stop();
        Console.WriteLine($"Custom quicksort time: {stopwatch.Elapsed.TotalSeconds:F4} seconds");

        stopwatch.Restart();
        Array.Sort(dataTemp);
        stopwatch.Stop();
        Console.WriteLine($"Built-in Array.Sort() time: {stopwatch.Elapsed.TotalSeconds:F4} seconds");

       
        for (int i = 0; i < sortedArrQuick.Length; i++)
        {
            if (sortedArrQuick[i] != dataTemp[i])
            {
                Console.WriteLine("Test failed: The results do not match!");
                return;
            }
        }
        Console.WriteLine("Test passed: Both sorting methods give the same result.");
    }
}
