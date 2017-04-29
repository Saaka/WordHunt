using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordHunt
{
    public static class ListExtensions
    {
        public static List<T> Split<T>(this List<T> list, int index)
        {
            var first = list.Take(index).ToList();
            list.RemoveRange(0, index);

            return first;
        }
    }
}
