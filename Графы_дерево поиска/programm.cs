using System;
using System.Diagnostics;

public class TreeNode
{
    public int Value;
    public TreeNode Left;
    public TreeNode Right;

    public TreeNode(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BinarySearchTree
{
    private TreeNode root;

    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    private TreeNode InsertRec(TreeNode root, int value)
    {
        if (root == null)
        {
            root = new TreeNode(value);
            return root;
        }

        if (value < root.Value)
            root.Left = InsertRec(root.Left, value);
        else if (value > root.Value)
            root.Right = InsertRec(root.Right, value);

        return root;
    }

    public bool Search(int value)
    {
        return SearchRec(root, value);
    }

    private bool SearchRec(TreeNode root, int value)
    {
        if (root == null)
            return false;

        if (value == root.Value)
            return true;

        return value < root.Value
            ? SearchRec(root.Left, value)
            : SearchRec(root.Right, value);
    }

    public void InOrderTraversal(Action<int> action)
    {
        InOrderRec(root, action);
    }

    private void InOrderRec(TreeNode root, Action<int> action)
    {
        if (root != null)
        {
            InOrderRec(root.Left, action);
            action(root.Value);
            InOrderRec(root.Right, action);
        }
    }
}




class Program
{
    static void Main()
    {
        BinarySearchTree bst = new BinarySearchTree();
        int numberOfElements = 1000000;
        Random random = new Random();

        
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < numberOfElements; i++)
        {
            bst.Insert(random.Next(1, numberOfElements * 10)); 
        }
        stopwatch.Stop();
        Console.WriteLine($"Вставка {numberOfElements} элементов заняла: {stopwatch.ElapsedMilliseconds} мс");

     
	 
        stopwatch.Restart();
        for (int i = 0; i < numberOfElements; i++)
        {
            bst.Search(random.Next(1, numberOfElements * 10));
        }
        stopwatch.Stop();
        Console.WriteLine($"Поиск {numberOfElements} элементов занял: {stopwatch.ElapsedMilliseconds} мс");



        stopwatch.Restart();
        bst.InOrderTraversal(value => {25}); //или другое значение
        stopwatch.Stop();
        Console.WriteLine($"Обход в порядке возрастания занял: {stopwatch.ElapsedMilliseconds} мс");
    }
}
