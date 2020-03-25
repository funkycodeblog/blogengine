using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public interface IMarkdownPreprocessor
    {
        string Fix(string str);

    }
}
