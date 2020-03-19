using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunkyCode.Blog.App.Internals
{ 

    public class SynchroItem<T, U>
       where T : class
       where U : class
    {

        public T First { get; set; }
        public U Second { get; set; }

        public bool IsMatched => (null != First && null != Second);

        public bool IsFirstUnmatched => (null != First && null == Second);

        public bool IsSecondUnmatched => (null == First && null != Second);
    }


    public static class SynchroTableExtensions
    {

        public static List<W> GetMatched<W, T, U>(this List<W> table) where W : SynchroItem<T, U> where T: class where U: class
        {
            var matched = table.Where(i => i.IsMatched).ToList();
            return matched;
        }

        public static List<T> GetFirstUnmatched<W, T, U>(this List<W> table) where W : SynchroItem<T, U> where T : class where U : class
        {
            var unmatchedFirst = table
                .Where(i => i.IsFirstUnmatched)
                .Select(i => i.First)
                .ToList();

            return unmatchedFirst;
        }

        public static List<U> GetSecondUnmatched<W, T, U>(this List<W> table) where W : SynchroItem<T, U> where T : class where U : class
        {
            var unmatchedSecond = table
                .Where(i => i.IsSecondUnmatched)
                .Select(i => i.Second)
                .ToList();

            return unmatchedSecond;
        }
    }

    

    public class SynchroTableFactory<T, U>
        where T : class
        where U : class
    {

        public List<W> CreateTable<W>(List<T> first, List<U> seconds, Func<T, U, bool> matchCondition) where W : SynchroItem<T, U>
        {

            List<W> table = new List<W>();

            List<T> firstMatched = new List<T>();
            List<U> secondMatched = new List<U>();


            foreach (T t in first)
            {

                foreach (U u in seconds)
                {
                    bool isMatched = matchCondition(t, u);
                    if (isMatched)
                    {
                        if (!firstMatched.Contains(t))
                            firstMatched.Add(t);

                        if (!secondMatched.Contains(u))
                            secondMatched.Add(u);

                        W w = Activator.CreateInstance<W>();
                        w.First = t;
                        w.Second = u;
                        table.Add(w);


                    }
                }

            }

            var firstUnmatched = first.Except(firstMatched).ToList();
            var secondUnmatched = seconds.Except(secondMatched).ToList();

            foreach (T t in firstUnmatched)
            {
                W w = Activator.CreateInstance<W>();
                w.First = t;
                w.Second = null;
                table.Add(w);
            }

            foreach (U u in secondUnmatched)
            {
                W w = Activator.CreateInstance<W>();
                w.First = null;
                w.Second = u;
                table.Add(w);
            }

            return table;
        }

        public List<W> GetMatched<W>(List<W> table) where W : SynchroItem<T, U>
        {
            List<W> matched = table.Where(i => i.IsMatched).ToList();
            return matched;
        }

        public List<T> GetFirstUnmatched<W>(List<W> table) where W : SynchroItem<T, U>
        {
            List<T> unmatchedFirst = table
                .Where(i => i.IsFirstUnmatched)
                .Select(i => i.First)
                .ToList();

            return unmatchedFirst;
        }

        public List<U> GetSecondUnmatched<W>(List<W> table) where W : SynchroItem<T, U>
        {
            List<U> unmatchedSecond = table
                .Where(i => i.IsSecondUnmatched)
                .Select(i => i.Second)
                .ToList();

            return unmatchedSecond;
        }



    }
}
