using System;
using System.Reflection;

namespace Assets.Scripts.Miscellaneous
{
    public static partial class Extensions
    {
        public enum InheritanceOptions
        {
            Strict,
            Derived
        }

        public static bool Is<T>(this object item, InheritanceOptions options = InheritanceOptions.Derived)
        {
            switch (options)
            {
                case InheritanceOptions.Strict:
                    return item.GetType() == typeof(T);

                case InheritanceOptions.Derived:
                    return item is T;

                default:
                    throw new ArgumentOutOfRangeException(nameof(options), options, null);
            }
        }

        public static bool Is(this Type t1, Type t2, InheritanceOptions options = InheritanceOptions.Derived)
        {
            switch (options)
            {
                case InheritanceOptions.Strict:
                    return t1 == t2;

                case InheritanceOptions.Derived:
                    return t2.GetTypeInfo().IsAssignableFrom(t1.GetTypeInfo());

                default:
                    throw new ArgumentOutOfRangeException(nameof(options), options, null);
            }
        }
    }
}