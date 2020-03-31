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
            input = c1 == 65279 ? input.Substring(1) : input;
            
            input = input.Replace(@"<!-- #header -->\r\n", "<!-- #header -->\r\n#####");
            
            return input;

        }
    }
}
