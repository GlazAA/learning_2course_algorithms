using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class ClosestPair
{
    
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    //  расчет расстояния между двумя точками
    private static double Distance(Point p1, Point p2)
    {
        return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }

    //  нахождение ближайшей пары точек 
    public static (Point, Point, double) BruteForceClosestPair(List<Point> points)
    {
        if (points == null || points.Count < 2)
        {
            return (default(Point), default(Point), double.MaxValue);
        }

        Point p1Result = default(Point), p2Result = default(Point);
        double minDistance = double.MaxValue;
        int n = points.Count;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double distance = Distance(points[i], points[j]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    p1Result = points[i];
                    p2Result = points[j];
                }
            }
        }

        return (p1Result, p2Result, minDistance);
    }


     // Метод разделяй и властвуй
    public static (Point, Point, double) ClosestPairDivideAndConquer(List<Point> points)
    {
        if (points == null || points.Count < 2)
        {
            return (default(Point), default(Point), double.MaxValue);
        }
        // Сортируем точки по координате x для разделения и слияния
        List<Point> sortedByX = points.OrderBy(p => p.X).ToList();

        return FindClosestPair(sortedByX);
    }

     private static (Point, Point, double) FindClosestPair(List<Point> points)
     {
         int n = points.Count;

        if (n <= 3)
        {
            return BruteForceClosestPair(points);
        }

        int mid = n / 2;
        List<Point> leftHalf = points.GetRange(0, mid);
        List<Point> rightHalf = points.GetRange(mid, n - mid);


        (Point, Point, double) leftResult = FindClosestPair(leftHalf);
        (Point, Point, double) rightResult = FindClosestPair(rightHalf);

        double delta = Math.Min(leftResult.Item3, rightResult.Item3);

        (Point, Point, double) stripResult = FindClosestPairInStrip(points, delta, mid);

        if(stripResult.Item3 < delta)
        {
            return stripResult;
        }
        else if(leftResult.Item3 < rightResult.Item3)
        {
            return leftResult;
        }
        else
        {
             return rightResult;
        }
    }


    // поиск ближайшей пары в полосе
    private static (Point, Point, double) FindClosestPairInStrip(List<Point> points, double delta, int mid)
    {
       Point midPoint = points[mid];
       List<Point> strip = points.Where(p => Math.Abs(p.X - midPoint.X) < delta).OrderBy(p => p.Y).ToList();

       int size = strip.Count;
       double minDistance = delta;
       Point p1Result = default(Point), p2Result = default(Point);


        for (int i = 0; i < size; i++)
        {
            for(int j = i + 1; j < size && (strip[j].Y - strip[i].Y) < minDistance; j++)
            {
                double distance = Distance(strip[i], strip[j]);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    p1Result = strip[i];
                    p2Result = strip[j];
                }
            }
        }
        
        return (p1Result, p2Result, minDistance);
    }


    public static void Main(string[] args)
    {
         // маленькие значения
        Console.WriteLine("Маленький набор данных:");
        List<Point> smallPoints = new List<Point>()
        {
            new Point { X = 1, Y = 1 },
            new Point { X = 2, Y = 2 },
            new Point { X = 1, Y = 3 },
            new Point { X = 3, Y = 3 },
            new Point { X = 4, Y = 1 }
        };
        TestClosestPair(smallPoints, "BruteForce");
        TestClosestPair(smallPoints, "DivideAndConquer");
       

        // (10000 точек)
        Console.WriteLine("\nБольшой набор данных:");
        List<Point> largePoints = GenerateRandomPoints(10000);
        TestClosestPair(largePoints, "BruteForce"); 
        TestClosestPair(largePoints, "DivideAndConquer");

    }
      private static void TestClosestPair(List<Point> points, string method)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        (Point p1, Point p2, double minDistance) result;

        if (method == "BruteForce")
        {
            result = BruteForceClosestPair(points);
             if (points.Count < 100) {
                  Console.WriteLine("Брутфорс метод:");
             }
        }
        else
        {
            result = ClosestPairDivideAndConquer(points);
            if (points.Count < 100) {
                   Console.WriteLine("Метод разделяй и властвуй:");
            }
        }
        stopwatch.Stop();

         if (points.Count < 100)
        {
             Console.WriteLine($"Ближайшая пара точек: ({result.p1.X}, {result.p1.Y}), ({result.p2.X}, {result.p2.Y})");
             Console.WriteLine($"Минимальное расстояние: {result.minDistance}");
        }
        else
        {
            Console.WriteLine($"Минимальное расстояние : {result.minDistance}");
        }
        Console.WriteLine($"Время выполнения ({method}): {stopwatch.ElapsedMilliseconds} мс");
    }


    private static List<Point> GenerateRandomPoints(int numPoints)
    {
        Random random = new Random();
        List<Point> points = new List<Point>();

        for (int i = 0; i < numPoints; i++)
        {
            points.Add(new Point { X = random.NextDouble() * 100, Y = random.NextDouble() * 100 });
        }
        return points;
    }
}