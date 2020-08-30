using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Internals
{
    public interface IParser<T>
    {
        T Parse(string str);
        bool TryParse(string str, out T result);
    }
}
