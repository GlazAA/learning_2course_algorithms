using System;

class Program
{
    static void Main(string[] args)
    {
       
        Console.Write("Введите размер массива: ");
        int size = int.Parse(Console.ReadLine());

        int[] array = new int[size];
        Random random = new Random();
  
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(1, 1001);
        }

        Console.WriteLine("Сгенерированный массив: ");
        Console.WriteLine(string.Join(", ", array));

        Console.Write("Введите число для поиска: ");
		
        int target = int.Parse(Console.ReadLine());
        int index = LinearSearch(array, target);
		
        if (index != -1)
        {
            Console.WriteLine($"Число {target} найдено по индексу: {index}");
        }
        else
        {
            Console.WriteLine($"Число {target} не найдено в массиве.");
        }
    }

    static int LinearSearch(int[] arr, int target)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == target)
            {
                return i; 
            }
        }
        return -1; 
    }
}
