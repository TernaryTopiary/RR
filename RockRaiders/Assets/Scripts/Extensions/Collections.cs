using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Cosmic.Space;

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

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T target)
        {
            return source.Concat(target.EnqueueInArray());
        }

        public static IEnumerable<T> Concat<T>(this T source, IEnumerable<T> target)
        {
            var list = new List<T>() { source };
            list.AddRange(target);
            return list;
        }

        public static T[] Spin<T>(this T[] source, RotationalOrientation direction)
        {
            if (source.Length <= 1) return source;
            switch(direction)
            {
                case RotationalOrientation.Clockwise:
                    return source.Skip(1).Concat(source.First()).ToArray();
                case RotationalOrientation.Anticlockwise:
                    return source.Last().EnqueueInArray().Concat(source.Take(source.Length - 1)).ToArray();
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}