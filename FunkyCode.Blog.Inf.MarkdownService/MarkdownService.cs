using System;
using System.Threading.Tasks;
using FunkyCode.Blog.App;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using Markdig;

namespace FunkyCode.Blog.Inf.MarkdownService
{
    public class MarkdownService : IMarkdownService
    {
        private readonly IMarkdownPreprocessor _markdownFixer;

        public MarkdownService(IMarkdownPreprocessor markdownFixer)
        {
            _markdownFixer = markdownFixer;
        }

        public Task<string> ConvertToHtml(string markdownContent)
        {

            var preprocessed = _markdownFixer.Fix(markdownContent);
            
            var result = Markdown.ToHtml(preprocessed);

            var withParagraphs = $"{result}<br/><br/><br/>";

            return Task.FromResult(withParagraphs);
        }
    }
}
