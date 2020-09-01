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
        private readonly IBlogPostMetadataResolver _metadataresolver;
        private readonly IImageUrlResolver _imageUrlResolver;

        public MarkdownService(IMarkdownPreprocessor markdownFixer, IArticleItemsProcessor articleItemsProcessor, IBlogPostMetadataResolver metadataresolver, IImageUrlResolver imageUrlResolver)
        {
            _markdownFixer = markdownFixer;
            _articleItemsProcessor = articleItemsProcessor;
            _metadataresolver = metadataresolver;
            _imageUrlResolver = imageUrlResolver;
        }

        public Task<string> ConvertToHtml(string markdownContent)
        {

            var metadata = _metadataresolver.Resolve(markdownContent);

            var preprocessed = _markdownFixer.Fix(markdownContent);

            if (metadata.Result.Attributes.Contains(BlogPostMetadata.HasCaptions))
                preprocessed = _articleItemsProcessor.Process(preprocessed);
            
            var result = Markdown.ToHtml(preprocessed);

            result = _imageUrlResolver.GetWithImageUrlResolved(result, metadata.Result.Id);

            var withParagraphs = $"{result}<br/><br/><br/>";

            return Task.FromResult(withParagraphs);
        }
    }
}
