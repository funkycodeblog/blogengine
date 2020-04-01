using System;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;

namespace FunkyCode.Blog.App.Internals.Tools
{
    public class MarkdownPreprocessor : IMarkdownPreprocessor
    {
        public string Fix(string input)
        {
            var c1 = input[0];
            input = c1 == 65279 ? input.Substring(1) : input;

            var replace = $"<!-- #header -->{Environment.NewLine}";

            var result = input.Replace(replace, "<!-- #header -->\r\n#### ", StringComparison.CurrentCultureIgnoreCase);
            
            return result;

        }
    }
}
