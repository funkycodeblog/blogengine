using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Internals.Map
{
    public class TagMapper : ITagMapper
    {
        public string[] Map(string first)
        {
            if (string.IsNullOrEmpty(first)) return new string[0];
            return first.Split(';'); 
        }

        public string Map(string[] second)
        {
            if (second == null) return string.Empty;
            return string.Join(';', second);
        }

        public List<string[]> Map(List<string> firstCollection)
        {
            throw new NotImplementedException();
        }

        public List<string> Map(List<string[]> secondCollection)
        {
            throw new NotImplementedException();
        }
    }
}
