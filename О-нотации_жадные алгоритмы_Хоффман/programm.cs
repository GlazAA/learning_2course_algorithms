using System;
using System.Collections.Generic;
using System.Linq;

class HuffmanNode
{
    public char Character { get; set; }
    public int Frequency { get; set; }
    public HuffmanNode Left { get; set; }
    public HuffmanNode Right { get; set; }

    public HuffmanNode(char character, int frequency)
    {
        Character = character;
        Frequency = frequency;
    }
}

class HuffmanTree
{
    public static HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencies)
    {
        var priorityQueue = new SortedSet<HuffmanNode>(Comparer<HuffmanNode>.Create((x, y) =>
        {
            int result = x.Frequency.CompareTo(y.Frequency);
            if (result == 0) return x.Character.CompareTo(y.Character);
            return result;
        }));

        foreach (var kvp in frequencies)
        {
            priorityQueue.Add(new HuffmanNode(kvp.Key, kvp.Value));
        }

        while (priorityQueue.Count > 1)
        {
            var left = priorityQueue.Min;
            priorityQueue.Remove(left);
            var right = priorityQueue.Min;
            priorityQueue.Remove(right);

            var merged = new HuffmanNode('\0', left.Frequency + right.Frequency)
            {
                Left = left,
                Right = right
            };
            priorityQueue.Add(merged);
        }

        return priorityQueue.Min;
    }

    public static Dictionary<char, string> GenerateCodes(HuffmanNode root)
    {
        var codes = new Dictionary<char, string>();
        GenerateCodesRecursively(root, "", codes);
        return codes;
    }

    private static void GenerateCodesRecursively(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        if (node == null) return;

        if (node.Left == null && node.Right == null)
        {
            codes[node.Character] = code;
        }

        GenerateCodesRecursively(node.Left, code + "0", codes);
        GenerateCodesRecursively(node.Right, code + "1", codes);
    }
}

class Program
{
    static void Main(string[] args)
    {
       
        TestHuffmanAlgorithm();
    }

    static void TestHuffmanAlgorithm()
    {
        var frequencies = new Dictionary<char, int>
        {
            { 'a', 5000 },
            { 'b', 9000 },
            { 'c', 12000 },
            { 'd', 13000 },
            { 'e', 16000 },
            { 'f', 45000 },
            { 'g', 25000 },
            { 'h', 30000 },
            { 'i', 20000 },
            { 'j', 35000 }
        };

        var huffmanTree = HuffmanTree.BuildHuffmanTree(frequencies);
        var huffmanCodes = HuffmanTree.GenerateCodes(huffmanTree);

        Console.WriteLine("Символы и их коды Хаффмана:");
        foreach (var kvp in huffmanCodes)
        {
            Console.WriteLine($"Символ: {kvp.Key}, Код: {kvp.Value}");
        }
    }
}
