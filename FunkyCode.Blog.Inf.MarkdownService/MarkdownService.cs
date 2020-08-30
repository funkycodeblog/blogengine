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
        private readonly IArticleItemsProcessor _articleItemsProcessor;

        public MarkdownService(IMarkdownPreprocessor markdownFixer, IArticleItemsProcessor articleItemsProcessor)
        {
            _markdownFixer = markdownFixer;
            _articleItemsProcessor = articleItemsProcessor;
        }

        public Task<string> ConvertToHtml(string markdownContent)
        {

            var preprocessed = _markdownFixer.Fix(markdownContent);

            var withCaptions = _articleItemsProcessor.Process(preprocessed);
            
            var result = Markdown.ToHtml(withCaptions);

            var withParagraphs = $"{result}<br/><br/><br/>";

            return Task.FromResult(withParagraphs);
        }
    }
}
