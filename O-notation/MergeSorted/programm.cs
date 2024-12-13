using System;
using System.Collections.Generic;

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; }
}

public class Solution
{
    public ListNode MergeKLists(ListNode[] lists)
    {

        var minHeap = new SortedSet<(int, ListNode)>(Comparer<(int, ListNode)>.Create((x, y) => {
            if (x.Item1 != y.Item1) return x.Item1.CompareTo(y.Item1);
            return ReferenceEquals(x.Item2, y.Item2) ? 0 : 1; 
        }));

        
        foreach (var list in lists)
        {
            if (list != null)
            {
                minHeap.Add((list.val, list));
            }
        }

      
        ListNode dummy = new ListNode(0);
        ListNode current = dummy;


        while (minHeap.Count > 0)
        {
            var (val, node) = minHeap.Min;
            minHeap.Remove(minHeap.Min); 

     
            current.next = new ListNode(val);
            current = current.next;

            if (node.next != null)
            {
                minHeap.Add((node.next.val, node.next));
            }
        }

        return dummy.next; 
    }
}

public class Program
{
    public static void Main()
    {
        // пример 1
        var lists = new ListNode[] {
            CreateList(new int[] { 1, 4, 5 }),
            CreateList(new int[] { 1, 3, 4 }),
            CreateList(new int[] { 2, 6 })
        };
        var result = new Solution().MergeKLists(lists);
        PrintList(result); //  1->1->2->3->4->4->5->6

        // пример 2
        lists = new ListNode[] { };
        result = new Solution().MergeKLists(lists);
        PrintList(result); // (пусто)

        // пример 3
        lists = new ListNode[] { CreateList(new int[] { }) };
        result = new Solution().MergeKLists(lists);
        PrintList(result); // (пусто)

        // тесты
        var largeLists = new ListNode[10000];
        for (int i = 0; i < 10000; i++)
        {
            largeLists[i] = CreateList(GetSortedArray(i)); 
        }
        result = new Solution().MergeKLists(largeLists);
        Console.WriteLine($"Дллина merge листа: {GetListLength(result)}");
    }

    private static ListNode CreateList(int[] values)
    {
        ListNode dummy = new ListNode(0);
        ListNode current = dummy;
        foreach (var val in values)
        {
            current.next = new ListNode(val);
            current = current.next;
        }
        return dummy.next;
    }

    private static int[] GetSortedArray(int index)
    {
        
        int size = 500; 
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = index * size + i; 
        }
        return arr;
    }

    private static void PrintList(ListNode node)
    {
        while (node != null)
        {
            Console.Write(node.val + (node.next != null ? "->" : ""));
            node = node.next;
        }
        Console.WriteLine();
    }

    private static int GetListLength(ListNode head)
    {
        int length = 0;
        while (head != null)
        {
            length++;
            head = head.next;
        }
        return length;
    }
}
