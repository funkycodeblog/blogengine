using System;
using System.Threading.Tasks;
using FunkyCode.Blog.App;
using Markdig;

namespace FunkyCode.Blog.Inf.MarkdownService
{
    public class MarkdownService : IMarkdownService
    {
        public Task<string> ConvertToHtml(string markdownContent)
        {
            var result = Markdown.ToHtml(markdownContent);
            return Task.FromResult(result);
        }
    }
}
