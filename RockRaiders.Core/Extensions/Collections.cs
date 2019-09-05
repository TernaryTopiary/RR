using System;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Extensions
{
    public static class Collections
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static T[] EnqueueInArray<T>(this T item)
        {
            return new[] { item };
        }

        public static List<T> EnqueueInList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T excluded)
        {
            return source.Except(excluded.EnqueueInArray());
        }
    }
}