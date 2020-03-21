using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;

namespace FunkyCode.Blog.App.Internals.Tools
{
    public class BlockPostMetadataResolver : IBlogPostMetadataResolver
    {
        public ProcessingResult<BlogPostMetadata> Resolve(string blog)
        {

            var result = new ProcessingResult<BlogPostMetadata>();

            var idPattern = "<!-- Id: (?<match>.*) -->";
            var datePattern = "<!-- Date: (?<match>.*) -->";
            var categoriesPattern = "<!-- Categories: (?<match>.*) -->";
            var headerPattern = "<!-- #header -->(?<match>(?s).*)<!-- #endheader -->";

            var titleToProcess = blog.Split(Environment.NewLine).FirstOrDefault();
            var title = titleToProcess.Replace("# ", "");

            var id = GetMatched(blog, idPattern);
            var date = GetMatched(blog, datePattern).Trim();
            var categories = GetMatched(blog, categoriesPattern);
            var header = GetMatched(blog, headerPattern);
            var headerNoNewLines = header.Replace(Environment.NewLine, "");

            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.Title), title);
            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.Id), id);
            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.Header), header);
            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.Categories), categories);
            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.PublishedDate), date);
            
            var categoriesAsList = categories.Split(',')
                .Select(i => i.Trim())
                .ToListSafe();

            var datetime = new DateTime(
                int.Parse(date.Substring(0, 4)),
                int.Parse(date.Substring(4, 2)),
                int.Parse(date.Substring(6, 2)));

            result.Result =  new BlogPostMetadata
            {
                Id = id,
                Title = title,
                Header = headerNoNewLines,
                Categories = categoriesAsList,
                PublishedDate = datetime
            };

            result.Status = result.Messages.Count == 0 ? ProcessingStatus.Ok : ProcessingStatus.Error;
            return result;
        }

        string GetMatched(string content, string pattern)
        {
            var isMatched = RegexHelper.Match(content, pattern, out var result);
            var match = result["match"];
            return match.Trim();
        }

    }
}
