using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class LanguageExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> objs, Action<T> ac)
        {
            foreach (var obj in objs)
            {
                ac(obj);
            }
        }
    }
}
