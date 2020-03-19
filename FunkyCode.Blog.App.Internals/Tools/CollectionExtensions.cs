using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static class CommonExtensions
    {
        public static bool IsTrue(this bool? item)
        {
            return item.HasValue && item.Value;
        }

        public static List<T> AsList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static T[] AsArray<T>(this T item)
        {
            var array = new T[1];
            array[0] = item;
            return array;
        }

        public static List<T> ToListSafe<T>(this IEnumerable<T> collection)
        {
            if (null == collection) return new List<T>();
            return collection.ToList();
        }

        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        public static bool IsNotNullNorEmpty(this string item)
        {
            return !IsNullOrEmpty(item);
        }

        public static string TrimAndToLowerSafe(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            return str.ToLower().Trim();
        }


        public static string RemoveDiacriticsSafe(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;

            string without = str
                .Replace('ą', 'a')
                .Replace('Ą', 'A')
                .Replace('ć', 'c')
                .Replace('Ć', 'C')
                .Replace('ę', 'e')
                .Replace('Ę', 'E')
                .Replace('ł', 'l')
                .Replace('Ł', 'L')
                .Replace('ń', 'n')
                .Replace('Ń', 'N')
                .Replace('ó', 'o')
                .Replace('Ó', 'O')
                .Replace('ś', 's')
                .Replace('Ś', 'S')
                .Replace('ż', 'z')
                .Replace('Ż', 'Z')
                .Replace('ź', 'z')
                .Replace('Ź', 'Z');

            return without;

        }


    }
}
