using UnityEngine;
using System.Collections;

public class HelperMethods
{
    public static int[,] RotateMatrix(int[,] arr, int n)
    {
        int width = n;
        int depth = n;
        int[,] re = new int[width, depth];
        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                re[j, depth - i - 1] = arr[i, j];
            }
        }
        return re;

        //    int[,] newArray = new int[n, n];

        //    for (int i = n-1; i >= 0; --i)
        //    {
        //        for (int j = 0; j < n; ++j)
        //        {
        //            newArray[j, n - i] = matrix[i, j];
        //        }
        //    }

        //    return newArray;
        //}
    }
}

public class Tuple<T1, T2>
{
    public T1 Item1 { get; private set; }
    public T2 Item2 { get; private set; }
    internal Tuple(T1 item1, T2 item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}

public static class Tuple
{
    public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
    {
        var tuple = new Tuple<T1, T2>(first, second);
        return tuple;
    }
}
