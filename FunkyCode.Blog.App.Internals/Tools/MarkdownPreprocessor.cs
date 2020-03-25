using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;

namespace FunkyCode.Blog.App.Internals.Tools
{
    public class MarkdownPreprocessor : IMarkdownPreprocessor
    {
        public string Fix(string input)
        {
            var c1 = input[0];
            return c1 == 65279 ? input.Substring(1) : input;

        }
    }
}
