using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        // Словарь для хранения анаграмм(eat,tea и ate и тд)
        var anagrams = new Dictionary<string, List<string>>();

        foreach (var str in strs)
        {
            var sortedStr = new string(str.OrderBy(c => c).ToArray());

            if (!anagrams.ContainsKey(sortedStr))
            {
                anagrams[sortedStr] = new List<string>();
            }
            anagrams[sortedStr].Add(str);
        }
        return anagrams.Values.ToList();
    }

    static void Main(string[] args)
    {
        var strs1 = new string[] { "eat", "tea", "tan", "ate", "nat", "bat" };
        var result1 = GroupAnagrams(strs1);
        Console.WriteLine("пример 1:");
        PrintResult(result1);

        var strs2 = new string[] { "" };
        var result2 = GroupAnagrams(strs2);
        Console.WriteLine("пример 2:");
        PrintResult(result2);

        
        var strs3 = new string[] { "a" };
        var result3 = GroupAnagrams(strs3);
        Console.WriteLine("пример 3:");
        PrintResult(result3);

        
        var largeStrs = new string[10000];
        for (int i = 0; i < 10000; i++)
        {
            largeStrs[i] = "abc";
        }
        var largeResult = GroupAnagrams(largeStrs);
        Console.WriteLine($"на выводе: {largeResult.Count}"); // ожидаем 1 группу
    }

    private static void PrintResult(IList<IList<string>> result)
    {
        foreach (var group in result)
        {
            Console.WriteLine($"[{string.Join(", ", group)}]");
        }
    }
}
