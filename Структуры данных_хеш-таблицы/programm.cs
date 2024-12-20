using System;
using System.Diagnostics;

public class HashNode
{
    public int Key { get; set; }
    public string Value { get; set; }
    public HashNode Next { get; set; }

    public HashNode(int key, string value)
    {
        Key = key;
        Value = value;
        Next = null;
    }
}

public class HashTable
{
    private HashNode[] table;
    private int size;

    public HashTable(int size)
    {
        this.size = size;
        table = new HashNode[size];
    }

    private int GetHash(int key)
    {
        return key % size;
    }

    public void Insert(int key, string value)
    {
        int hashIndex = GetHash(key);
        HashNode newNode = new HashNode(key, value);

        if (table[hashIndex] == null)
        {
            table[hashIndex] = newNode;
        }
        else
        {
            HashNode current = table[hashIndex];
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode; // Сцепляем узлы
        }
    }

    public string Search(int key)
    {
        int hashIndex = GetHash(key);
        HashNode current = table[hashIndex];

        while (current != null)
        {
            if (current.Key == key)
            {
                return current.Value;
            }
            current = current.Next;
        }
        return null;
    }

    public bool Delete(int key)
    {
        int hashIndex = GetHash(key);
        HashNode current = table[hashIndex];
        HashNode previous = null;

        while (current != null)
        {
            if (current.Key == key)
            {
                if (previous == null)
                {
                    table[hashIndex] = current.Next; // удаление узлов
                }
                else
                {
                    previous.Next = current.Next; 
                }
                return true; 
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        int size = 10000; // Размер хеш-таблицы
        HashTable hashTable = new HashTable(size);
        Random random = new Random();

        // Тестирование вставки
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < 100000; i++) 
        {
            hashTable.Insert(random.Next(1, 1000000), $"Value {i}");
        }
        stopwatch.Stop();
        Console.WriteLine($"Вставка 100000 элементов заняла: {stopwatch.ElapsedMilliseconds} мс");

        // Тестирование поиска
        stopwatch.Restart();
        for (int i = 0; i < 100000; i++)
        {
            hashTable.Search(random.Next(1, 1000000));
        }
        stopwatch.Stop();
        Console.WriteLine($"Поиск 100000 элементов занял: {stopwatch.ElapsedMilliseconds} мс");

        // Тестирование удаления
        stopwatch.Restart();
        for (int i = 0; i < 100000; i++)
        {
            hashTable.Delete(random.Next(1, 1000000));
        }
        stopwatch.Stop();
        Console.WriteLine($"Удаление 100000 элементов заняло: {stopwatch.ElapsedMilliseconds} мс");
    }
}
