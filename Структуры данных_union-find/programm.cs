using System;
using System.Collections.Generic;
using System.Diagnostics;

public class UnionFind
{
    private Dictionary<int, int> parent;
    private Dictionary<int, int> rank;

    public UnionFind(int numberOfElements)
    {
        parent = new Dictionary<int, int>();
        rank = new Dictionary<int, int>();
        for (int i = 0; i < numberOfElements; i++)
        {
            parent[i] = i; 
            rank[i] = 0; 
        }
    }

 
    public int Find(int i)
    {
        if (parent[i] != i)
        {
            parent[i] = Find(parent[i]); 
        }
        return parent[i];
    }


    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
         
            if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }
        }
    }


    public static void TestUnionFind(int numElements, int numUnions)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        UnionFind uf = new UnionFind(numElements);

        Random random = new Random();
        for (int i = 0; i < numUnions; i++)
        {
            int x = random.Next(numElements);
            int y = random.Next(numElements);
             uf.Union(x,y); 
         }


        for (int i = 0; i < numElements; i++)
        {
           uf.Find(i);
         }
        stopwatch.Stop();

          Console.WriteLine($"Number of elements: {numElements}");
          Console.WriteLine($"Number of unions: {numUnions}");
          Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
         
    }

    public static void Main(string[] args)
    {
        // тесты 1
        Console.WriteLine("Small data test:");
        UnionFind ufSmall = new UnionFind(5);
        ufSmall.Union(0, 1);
        ufSmall.Union(2, 3);

        Console.WriteLine($"Find(0): {ufSmall.Find(0)}"); // 0 или 1
        Console.WriteLine($"Find(1): {ufSmall.Find(1)}"); // 0 или 1
        Console.WriteLine($"Find(2): {ufSmall.Find(2)}"); // 2 или 3
        Console.WriteLine($"Find(4): {ufSmall.Find(4)}"); // 4


         // тест 2
         Console.WriteLine("\nLarge data test:");
         TestUnionFind(10000, 100000);

         Console.WriteLine("\nVery Large data test:");
         TestUnionFind(100000, 1000000);
     }
}