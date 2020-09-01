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
            var pagePattern = "<!-- Page -->";
            var attributePattern = "<!-- Meta: (?<match>.*) -->";

            var titleToProcess = blog.Split(Environment.NewLine).FirstOrDefault();
            var title = titleToProcess.Replace("# ", "");

            var id = GetMatched(blog, idPattern);
            var isArticle = !blog.Contains(pagePattern);
            
            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.Title), title);
            result.AddRequiredMessageIfNullOrEmpty(nameof(BlogPostMetadata.Id), id);

            if (!isArticle)
            {
                result.Result = new BlogPostMetadata
                {
                    Id = id,
                    Title = title,
                    PostType = BlogPostMetadata.PostTypeEnum.Page
                };

                result.Status = result.Messages.Count == 0 ? ProcessingStatus.Ok : ProcessingStatus.Error;
                return result;
            }

            var date = GetMatched(blog, datePattern).Trim();
            var categories = GetMatched(blog, categoriesPattern);
            var header = GetMatched(blog, headerPattern);
            var attributesStr = GetMatched(blog, attributePattern);
            var attributes = new List<string>();
            if (null != attributesStr)
            {
                attributes = attributesStr
                    .Split(";")
                    .Select(s => s.Trim())
                    .ToList();
            }    

            var headerNoNewLines = header.Replace(Environment.NewLine, "");



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
                PublishedDate = datetime,
                PostType = BlogPostMetadata.PostTypeEnum.Article,
                Attributes = attributes.ToList()
            };

            result.Status = result.Messages.Count == 0 ? ProcessingStatus.Ok : ProcessingStatus.Error;
            return result;
        }

        string GetMatched(string content, string pattern)
        {
            var isMatched = RegexHelper.Match(content, pattern, out var result);
            if (!isMatched) return null;
            
            var match = result["match"];
            return match.Trim();
        }

    }
}
